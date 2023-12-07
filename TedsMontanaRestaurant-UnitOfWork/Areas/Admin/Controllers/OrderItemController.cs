using Microsoft.AspNetCore.Mvc;
using TedsMontanaRestaurantData.Repository.IRepository;
using TedsMontanaRestaurantModel;

namespace TedsMontanaRestaurant_UnitOfWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderItemController : Controller
    {
        private IUnitOfWork unitOfWork;
        public OrderItemController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<OrderItem> orderItemList = unitOfWork.OrderItem.GetAll();
            return View(orderItemList);
        }
        [HttpGet]
        public IActionResult Add() { return View(); }
        [HttpPost]
        public IActionResult Add(OrderItem orderItem)
        {
            unitOfWork.OrderItem.add(orderItem);
            unitOfWork.save();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var findId = unitOfWork.OrderItem.GetById(x => x.OrderItemId == id);

            return View(findId);


        }
        [HttpPost]
        public IActionResult Edit(OrderItem orderItem)
        {
            unitOfWork.OrderItem.Update(orderItem);
            unitOfWork.save();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var findid = unitOfWork.OrderItem.GetById(x => x.OrderItemId == id);
            unitOfWork.OrderItem.remove(findid);
            unitOfWork.save();
            return RedirectToAction("Index");
        }
    }
}
