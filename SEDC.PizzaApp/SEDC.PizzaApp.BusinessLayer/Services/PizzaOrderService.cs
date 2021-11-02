using SEDC.PizzaApp.BusinessLayer.Interfaces;
using SEDC.PizzaApp.BusinessModels.ViewModels;
using SEDC.PizzaApp.DataAccess.Repositories;
using SEDC.PizzaApp.Domain.Enums;
using SEDC.PizzaApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SEDC.PizzaApp.BusinessLayer.Services
{
    public class PizzaOrderService : IPizzaOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Pizza> _pizzaRepository;
        private readonly IRepository<User> _userRepository;

        public PizzaOrderService(IRepository<Order> orderRepository,
            IRepository<Pizza> pizzaRepository,
            IRepository<User> userRepository)
        {
            _orderRepository = orderRepository;
            _pizzaRepository = pizzaRepository;
            _userRepository = userRepository;
        }

        public OrdersViewModel GetAllOrders()
        {
            // Domain models
            List<Order> orders = _orderRepository.GetAll();
            // View Models
            // MAPPINT SECTION
            List<OrderItemViewModel> viewOrders = new List<OrderItemViewModel>();
            foreach (Order order in orders)
            {
                List<PizzaViewModel> pizzasView = new List<PizzaViewModel>();
                foreach (var pizzaOrder in order.PizzaOrders)
                {
                    pizzasView.Add(new PizzaViewModel()
                    {
                        Id = pizzaOrder.Pizza.Id,
                        Image = pizzaOrder.Pizza.Image,
                        Name = pizzaOrder.Pizza.Name,
                        Price = pizzaOrder.Pizza.Price,
                        Size = pizzaOrder.Pizza.Size
                    });
                };
                viewOrders.Add(new OrderItemViewModel()
                {
                    FirstName = order.User.FirstName,
                    LastName = order.User.LastName,
                    Id = order.Id,
                    Price = order.Price,
                    Pizzas = pizzasView
                });
            }

            var lastUser = _userRepository.GetAll().LastOrDefault();

            OrdersViewModel model = new OrdersViewModel()
            {
                LastPizza = GetLastOrder().PizzaOrders[0].Pizza.Name, //TODO: get last pizza
                MostPopularPizza = GetMostPopularPizza(),
                NameOfFirstCustomer = string.Format("{0} {1}", lastUser.FirstName, lastUser.LastName), // $"{lastUser.FirstName} {lastUser.LastName}";
                OrderCount = GetOrderCount(),
                Orders = viewOrders
            };
            return model;
            // We send mapped view model to the view
        }

        public MenuViewModel GetMenu()
        {
            var menu = _pizzaRepository.GetAll()
                .GroupBy(p => p.Name)
                .Select(x => x.First())
                .ToList();

            List<PizzaViewModel> listOfPizzas = new List<PizzaViewModel>();
            foreach (var piza in menu)
            {
                listOfPizzas.Add(new PizzaViewModel
                {
                    Id = piza.Id,
                    Image = piza.Image,
                    Name = piza.Name,
                    Price = piza.Price,
                    Size = piza.Size
                });
            }
            var model = new MenuViewModel
            {
                Menu = listOfPizzas
            };

            return model;
        }

        public string GetMostPopularPizza()
        {
            var orders = _orderRepository.GetAll();

            var pizzas = orders
                .SelectMany(x => x.PizzaOrders)
                .ToList();

            var mostPopularPizza = pizzas
                .GroupBy(x => x.Pizza.Name)
                .OrderByDescending(x => x.Count())
                .FirstOrDefault()
                .Select(x => x.Pizza.Name)
                .FirstOrDefault();

            return mostPopularPizza;
        }

        public OrderDetailsViewModel GetOrderById(int id)
        {
            Order order = _orderRepository.GetById(id);

            if(order == null)
            {
                throw new Exception();
            }

            List<PizzaViewModel> pizzas = new List<PizzaViewModel>();
            foreach (PizzaOrder pizzaOrder in order.PizzaOrders)
            {
                pizzas.Add(new PizzaViewModel()
                {
                    Image = pizzaOrder.Pizza.Image,
                    Name = pizzaOrder.Pizza.Name,
                    Price = pizzaOrder.Pizza.Price,
                    Size = pizzaOrder.Pizza.Size
                });
            }

            OrderDetailsViewModel viewModel = new OrderDetailsViewModel()
            {
                Address = order.User.Address,
                Order = new OrderItemViewModel()
                {
                    Id = order.Id,
                    FirstName = order.User.FirstName,
                    Pizzas = pizzas,
                    LastName = order.User.LastName,
                    Price = order.Price
                },
                Phone = order.User.Phone
            };

            return viewModel;
        }

        public int MakeNewOrder(OrderViewModel orderVm)
        {
            var order = new Order();
            var pizzas = new List<PizzaOrder>();

            foreach (var pizzaVm in orderVm.Pizzas)
            {
                var pizzaOrder = new PizzaOrder()
                {
                    Pizza = GetPizzaFromMenu(pizzaVm.Name, pizzaVm.Size),
                    Order = order
                };
                pizzaOrder.PizzaId = pizzaOrder.Pizza.Id;
                pizzas.Add(pizzaOrder);
            }

            var user = new User
            {
                Address = orderVm.Address,
                FirstName = orderVm.FirstName,
                LastName = orderVm.LastName,
                Phone = orderVm.Phone.Value
            };

            order.PizzaOrders = pizzas;
            order.User = user;
            return _orderRepository.Insert(order);
        }

        private int GetOrderCount()
        {
            return _orderRepository.GetAll().Count;
        }

        private Order GetLastOrder()
        {
            var orders = _orderRepository.GetAll();
            return orders[orders.Count - 1];
        }

        private Pizza GetPizzaFromMenu(string name, PizzaSize size)
        {
            return _pizzaRepository.GetAll().FirstOrDefault(p => p.Name == name && p.Size == size);
        }
    }
}
