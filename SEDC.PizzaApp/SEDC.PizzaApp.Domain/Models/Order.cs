using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SEDC.PizzaApp.Domain.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

        public List<PizzaOrder> PizzaOrders { get; set; } = new List<PizzaOrder>();

        public double Price
        {
            get
            {
                //if(PizzaOrders == null)
                //{
                //    return 0;
                //}

                return PizzaOrders.Sum(o => o.Pizza.Price) + 2;
            }
        }
    }
}
