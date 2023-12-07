using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TedsMontanaRestaurantData.Repository.IRepository;
using TedsMontanaRestaurantModel;

namespace TedsMontanaRestaurant_UnitOfWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RestaurantController : Controller
    {

        private IUnitOfWork unitOfWork;
        public RestaurantController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Restaurant> restaurantList = unitOfWork.Restaurant.GetAll();
            return View(restaurantList);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Restaurant restaurant)
        {
            unitOfWork.Restaurant.add(restaurant);
            unitOfWork.save();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var findId = unitOfWork.Restaurant.GetById(x => x.RestaurantId == id);
            return View(findId);
        }
        [HttpPost]
        public IActionResult Edit(Restaurant restaurant)
        {
            unitOfWork.Restaurant.Update(restaurant);
            unitOfWork.save();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var findId = unitOfWork.OrderItem.GetById(x => x.OrderId == id);
            unitOfWork.OrderItem.Update(findId);
            unitOfWork.save();
            return RedirectToAction("Index");
        }

    }
}
