namespace SEDC.PizzaApp.BusinessModels.ViewModels
{
    public class OrderDetailsViewModel
    {
        public string Address { get; set; }
        public long Phone { get; set; }
        public OrderItemViewModel Order { get; set; }
    }
}
