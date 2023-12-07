using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TedsMontanaRestaurantModel.ViewModels
{
    public class MealVm
    {
        public Meal Meal { get; set; }
       public IEnumerable<SelectListItem> menuList { get; set; }
    }
}

