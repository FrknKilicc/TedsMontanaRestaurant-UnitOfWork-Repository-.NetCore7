using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TedsMontanaRestaurantModel.ViewModels
{
    public class OrderVM
    {
        public IEnumerable<Order> Order { get; set; }
        public IEnumerable<SelectListItem> customerList { get; set; }  
        public List<OrderItem> orderItemsList { get; set; }
    }
}
