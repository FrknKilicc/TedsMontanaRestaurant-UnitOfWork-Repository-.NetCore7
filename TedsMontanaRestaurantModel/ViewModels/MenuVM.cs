using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TedsMontanaRestaurantModel.ViewModels
{
    public class MenuVM
    {
        public Menu menu { get; set; }
        public IEnumerable<SelectListItem> mealList { get; set; }
        public IEnumerable<SelectListItem> restauranList { get; set; }
        
    }
}
