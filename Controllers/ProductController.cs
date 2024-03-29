using Eagles_Website.Models;
using Eagles_Website.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eagles_Website.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOFWork UnitOFWork;
        public ProductController(IUnitOFWork UnitOFWork)
        {
            this.UnitOFWork = UnitOFWork;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            var Products = UnitOFWork.ProductRepo.GetAll().ToList();
            return View("Index", Products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            var Categories = UnitOFWork.CategoryRepo.GetAll().ToList();
            ViewData["CategoryList"] = Categories.ToList();
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product Product)
        {
            try
            {
                UnitOFWork.ProductRepo.add(Product);
                UnitOFWork.ProductRepo.save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
