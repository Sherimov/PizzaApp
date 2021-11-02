using SEDC.PizzaApp.BusinessModels.ViewModels;
using SEDC.PizzaApp.Domain.Enums;
using SEDC.PizzaApp.Domain.Models;

namespace SEDC.PizzaApp.BusinessLayer.Interfaces
{
    public interface IPizzaOrderService
    {
        OrdersViewModel GetAllOrders();
        MenuViewModel GetMenu();
        string GetMostPopularPizza();
        OrderDetailsViewModel GetOrderById(int id);
        int MakeNewOrder(OrderViewModel orderVm);
    }
}
