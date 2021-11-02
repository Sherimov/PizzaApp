using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEDC.PizzaApp.Domain.Models
{
    public class PizzaOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }
    }
}