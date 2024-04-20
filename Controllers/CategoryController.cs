using Eagles_Website.Models;
using Eagles_Website.Repository.IRepository;
using Eagles_Website.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eagles_Website.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOFWork UnitOFWork;

        public CategoryController(IUnitOFWork UnitOFWork)
        {
            this.UnitOFWork = UnitOFWork;
        }

        // GET: CategoryController
        public ActionResult Index()
        {
            List<Category> Categories = UnitOFWork.CategoryRepo.GetAll().ToList();
            return View("Index",Categories);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(Category Category)
        {
            Category CategoryRepository = UnitOFWork.CategoryRepo.Get(C => C.ID == Category.ID, "Products");
            CategoryWithProductList CategoryVM = new CategoryWithProductList();
            CategoryVM.ID = CategoryRepository.ID;
            CategoryVM.Name = CategoryRepository.Name;
            CategoryVM.Description = CategoryRepository.Description;
            CategoryVM.Products = CategoryRepository.Products;

            return View("Details", CategoryVM);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category Category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UnitOFWork.CategoryRepo.add(Category);
                    UnitOFWork.CategoryRepo.save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("Create");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int ID)
        {
            Category Category = UnitOFWork.CategoryRepo.Get(C => C.ID == ID);
            return View("Edit",Category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category Category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UnitOFWork.CategoryRepo.update(Category);
                    UnitOFWork.CategoryRepo.save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("Edit");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int ID)
        {
            Category Category = UnitOFWork.CategoryRepo.Get(C => C.ID == ID);
            if (Category == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Delete",Category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Category Category)
        {
            try
            {
                if(Category != null)
                {
                    UnitOFWork.CategoryRepo.remove(Category);
                    UnitOFWork.CategoryRepo.save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("Delete");
                }
                
            }
            catch
            {
                return View();
            }
        }
    }
}
