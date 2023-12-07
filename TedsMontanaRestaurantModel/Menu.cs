using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TedsMontanaRestaurantModel
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }
        [Required]
        public string Name { get; set; }

        public string? Images {  get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public List<Meal> Meals { get; set; }
    }
}
