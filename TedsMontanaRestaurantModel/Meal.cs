using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TedsMontanaRestaurantModel
{
    public class Meal
    {
        [Key]
        public int MealId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string? Images { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
