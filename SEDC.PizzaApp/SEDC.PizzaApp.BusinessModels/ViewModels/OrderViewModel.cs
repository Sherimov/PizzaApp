using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SEDC.PizzaApp.BusinessModels.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public long? Phone { get; set; }
        [Required]
        public List<PizzaViewModel> Pizzas { get; set; }
    }
}
