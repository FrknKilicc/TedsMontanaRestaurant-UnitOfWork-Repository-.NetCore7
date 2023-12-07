using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using TedsMontanaRestaurantData.Repository.IRepository;
using TedsMontanaRestaurantModel.ViewModels;

namespace TedsMontanaRestaurant_UnitOfWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        public IUnitOfWork UnitOfWork { get; set; }
        private IWebHostEnvironment _webHostEnvironment;
        public MenuController(IUnitOfWork UnitOfWork, IWebHostEnvironment _webHostEnvironment)
        {
            this.UnitOfWork = UnitOfWork;
            this._webHostEnvironment = _webHostEnvironment;
        }
        public IActionResult Index()
        {
            var listMenus = UnitOfWork.Menu.GetAll();
            return View(listMenus);
        }
        public IActionResult Crup(int? id = 0)
        {
            MenuVM menuVM = new()
            {
                menu = new(),
                mealList = UnitOfWork.Meal.GetAll().Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.MealId.ToString(),
                }),
                restauranList = UnitOfWork.Restaurant.GetAll().Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.RestaurantId.ToString(),
                })
            };
            if (id == null || id <= 0)
            {
                return View(menuVM);
            }
            menuVM.menu = UnitOfWork.Menu.GetById(x => x.MenuId == id);
            if (menuVM.menu == null)
            {
                return View(menuVM);
            }
            return View(menuVM);
        }

        [HttpPost]
        public IActionResult Crup(MenuVM menuVM, IFormFile file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploadRooth = Path.Combine(wwwRootPath, @"img\MenuImages");
                var extension = Path.GetExtension(file.FileName);
                if (menuVM.menu.Images != null)
                {
                    var oldPicPath = Path.Combine(wwwRootPath, menuVM.menu.Images);
                    if (System.IO.File.Exists(oldPicPath))
                    {
                        System.IO.File.Delete(oldPicPath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(uploadRooth, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                menuVM.menu.Images = @"\img\MenuImages\" + fileName + extension;
            }
            if (menuVM.menu.MenuId <= 0)
            {
                UnitOfWork.Menu.add(menuVM.menu);
            }
            else
            {
                UnitOfWork.Menu.Update(menuVM.menu);
            }
            UnitOfWork.save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete (int? id)
        {
            if (id==0 || id== null)
            {
                return NotFound();
            }
            else
            {
                var findId = UnitOfWork.Menu.GetById(x => x.MenuId == id);
                UnitOfWork.Menu.remove(findId);
                UnitOfWork.save();
                return RedirectToAction("Index");
            }
        }
    }
}
