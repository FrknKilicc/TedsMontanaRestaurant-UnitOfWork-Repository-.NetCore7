using Microsoft.AspNetCore.Mvc;
using TedsMontanaRestaurantData.Repository.IRepository;
using TedsMontanaRestaurantModel;

namespace TedsMontanaRestaurant_UnitOfWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
       
        public readonly IUnitOfWork unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Customer> customerList = unitOfWork.Customer.GetAll();
            return View(customerList);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Customer customer)
        {
           unitOfWork.Customer.add(customer);
            unitOfWork.save();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var customerList = unitOfWork.Customer.GetById(x => x.CustomerId == id);
            return View(customerList);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            unitOfWork.Customer.Update(customer);
            unitOfWork.save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id) 
        {
            var findid = unitOfWork.Customer.GetById(x => x.CustomerId == id);
            unitOfWork.Customer.remove(findid);
            unitOfWork.save();
            return RedirectToAction("Index");   
        }
    }
}
