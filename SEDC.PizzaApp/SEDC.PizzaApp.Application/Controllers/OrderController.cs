using Microsoft.AspNetCore.Mvc;
using SEDC.PizzaApp.BusinessLayer.Interfaces;
using SEDC.PizzaApp.BusinessModels.ViewModels;
using System;
using System.Collections.Generic;

namespace SEDC.PizzaApp.Application.Controllers
{
    public class OrderController : Controller
    {
        private readonly IPizzaOrderService _pizzaOrderService;

        public OrderController(IPizzaOrderService pizzaOrderService)
        {
            _pizzaOrderService = pizzaOrderService;
        }

        [HttpGet("Orders")]
        public IActionResult Index()
        {
            var orderVm = _pizzaOrderService.GetAllOrders();
            return View(orderVm);
        }

        [HttpGet]
        public IActionResult Order(string error, int pizzas)
        {
            if(error != null)
            {
                return View("_Error");
            }

            var orderVM = new OrderViewModel();
            orderVM.Pizzas = new List<PizzaViewModel>();
            for (int i = 0; i < pizzas; i++)
            {
                orderVM.Pizzas.Add(new PizzaViewModel());
            }

            return View(orderVM);
        }

        [HttpPost]
        public IActionResult Order(OrderViewModel model)
        {
            // TODO: Make validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var orderId = _pizzaOrderService.MakeNewOrder(model);

            if(orderId != 0)
            {
                return View("_ThankYou");
            }

            return RedirectToAction("Order", model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var orderDetails = _pizzaOrderService.GetOrderById(id);
                return View(orderDetails);
            }
            catch (Exception)
            {
                return View("_Error");
            }
        }
    }
}
