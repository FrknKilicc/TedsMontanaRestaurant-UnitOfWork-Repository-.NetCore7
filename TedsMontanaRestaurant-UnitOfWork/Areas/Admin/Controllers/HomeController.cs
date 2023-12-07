using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TedsMontanaRestaurant_UnitOfWork.Models;

namespace TedsMontanaRestaurant_UnitOfWork.Areas.Controllers
{

    [Area("Admin")]
    public class HomeController :Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
