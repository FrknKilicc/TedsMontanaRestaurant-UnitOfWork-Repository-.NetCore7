using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using NuGet.Packaging.Signing;
using TedsMontanaRestaurantData.Repository.IRepository;
using TedsMontanaRestaurantModel;
using TedsMontanaRestaurantModel.ViewModels;

namespace TedsMontanaRestaurant_UnitOfWork.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MealController : Controller
    {
        private IUnitOfWork unitOfWork;
        private IWebHostEnvironment env;
        public MealController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            this.unitOfWork = unitOfWork;
            this.env=env;
        }
        public IActionResult Index()
        {
            var mealList=unitOfWork.Meal.GetAll();

            return View(mealList);
        }
        public IActionResult Crup(int? id = 0)
        {
            MealVm mealVm = new()
            {
                Meal = new(),
                menuList = unitOfWork.Menu.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.MenuId.ToString(),
                })
            };
            if (id == null || id <= 0)
            {
                return View(mealVm);
            }
            mealVm.Meal = unitOfWork.Meal.GetById(x => x.MealId == id);
            if (mealVm.Meal == null)
            {
                return View(mealVm);
            }
            return View(mealVm);
        }
        [HttpPost]
        public IActionResult Crup(MealVm mealvm, IFormFile file)
        {
            string wwwRootPath = env.WebRootPath;
            string uploadRoot=null;
            string fileName = null;
            string extension = null;
            if (file != null)
            {
                 fileName = Guid.NewGuid().ToString();
                 uploadRoot = Path.Combine(wwwRootPath, @"img\MealImages");
                 extension = Path.GetExtension(file.FileName);
                if (!string.IsNullOrEmpty(mealvm.Meal.Images))
                {
                    var oldPicPath = Path.Combine(wwwRootPath, mealvm.Meal.Images);
                    if (System.IO.File.Exists(oldPicPath))
                    {
                        System.IO.File.Delete(oldPicPath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(uploadRoot, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                mealvm.Meal.Images = @"\img\MealImages\" + fileName + extension;
            }
            else if (string.IsNullOrEmpty(mealvm.Meal.Images) && !string.IsNullOrEmpty(mealvm.Meal.MealId.ToString()))
            {
                var existingMeal = unitOfWork.Meal.GetById(x => x.MealId == mealvm.Meal.MealId);
                if (existingMeal != null && !string.IsNullOrEmpty(existingMeal.Images))
                {
                    mealvm.Meal.Images = existingMeal.Images;
                }
            }
            if (mealvm.Meal.MealId <= 0)
            {
                unitOfWork.Meal.add(mealvm.Meal);
            }
            else
            {
                unitOfWork.Meal.Update(mealvm.Meal);
            }
            unitOfWork.save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id==0)
            {
                return NotFound();
            }
            else
            {
                var findId = unitOfWork.Meal.GetById(x => x.MealId == id);
                unitOfWork.Meal.remove(findId);
                unitOfWork.save();
                return RedirectToAction("Index");   
            }
        }
    }
}
