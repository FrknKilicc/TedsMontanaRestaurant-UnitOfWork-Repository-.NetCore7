using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TedsMontanaRestaurantData.Repository;
using TedsMontanaRestaurantData.Repository.IRepository;
using TedsMontanaRestaurantModel;
using TedsMontanaRestaurantModel.ViewModels;

namespace TedsMontanaRestaurant_UnitOfWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private IUnitOfWork unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            OrderVM orderVM = new()
            {
                Order = unitOfWork.Orderr.GetAll(), 
                customerList = unitOfWork.Customer.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.CustomerId.ToString(),
                }),
            };

            return View(orderVM);
        }

        public IActionResult Add()
        {
            var customers = unitOfWork.Customer.GetAll().Select(y => new SelectListItem
            {
                Text = y.Name,
                Value = y.CustomerId.ToString(),
            });

            ViewBag.CustomerList = customers;

            return View();
        }

        [HttpPost]
        public IActionResult Add(Order order)
        {
            unitOfWork.Orderr.add(order);
            unitOfWork.save();
            return RedirectToAction("Index");
        }
       
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var order = unitOfWork.Orderr.GetById(x => x.OrderId == id);
            if (order == null || order.CustomerId == null)
            {
                return NotFound("deneme");
            }
            var customers = unitOfWork.Customer.GetAll().Select(y => new SelectListItem
            {
                Text = y.Name,
                Value = y.CustomerId.ToString(),
                Selected = (y.CustomerId == order.CustomerId)
            });
            ViewBag.CustomerList = customers;
            return View(order);
        }
        [HttpPost]
        public IActionResult Edit(Order order)
        {
            unitOfWork.Orderr.Update(order);
            unitOfWork.save();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var findId = unitOfWork.Orderr.GetById(x => x.OrderId == id);
            unitOfWork.Orderr.remove(findId);
            unitOfWork.save();
            return RedirectToAction("Index");
        }
    }
}
