using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TedsMontanaRestaurantData.Repository.IRepository;
using TedsMontanaRestaurantModel;

namespace TedsMontanaRestaurant_UnitOfWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PaymentController : Controller
    {
        private IUnitOfWork unitOfWork;
        public PaymentController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Payment> PaymentttList = unitOfWork.Paymentt.GetAll();
            return View(PaymentttList);
        }
        public IActionResult Add()
        {
            var orderList = unitOfWork.Orderr.GetAll().Select(x => new SelectListItem
            {
                Text = x.OrderId.ToString(),
                Value = x.OrderId.ToString()
            });
            ViewBag.orderList = orderList;  
            return View();
        }
        [HttpPost]
        public IActionResult Add(Payment payment)
        {
            unitOfWork.Paymentt.add(payment);
            unitOfWork.save();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var payment = unitOfWork.Paymentt.GetById(x => x.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }
            var orderList = unitOfWork.Orderr.GetAll().Select(x => new SelectListItem
            {
                Text = x.OrderId.ToString(),
                Value = x.OrderId.ToString(),
                Selected = (x.OrderId == payment.OrderId)
            });
            ViewBag.orderList = orderList;
            return View(payment);
        }
        [HttpPost]
        public IActionResult Edit(Payment payment)
        {
            unitOfWork.Paymentt.Update(payment);
            unitOfWork.save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var findId = unitOfWork.Paymentt.GetById(x => x.PaymentId == id);
            unitOfWork.Paymentt.remove(findId);
            unitOfWork.save();
            return RedirectToAction("Index");
        }
    }
}
