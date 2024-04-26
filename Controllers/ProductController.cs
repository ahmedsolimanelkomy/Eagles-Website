using Day1.Hubs;
using Eagles_Website.Models;
using Eagles_Website.Repository;
using Eagles_Website.Repository.IRepository;
using Eagles_Website.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Eagles_Website.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOFWork UnitOFWork;
        private readonly IWebHostEnvironment WebHostEnvironment;
        private readonly IHubContext<ProductHub> ProductHub;

        public ProductController(IUnitOFWork UnitOFWork, IWebHostEnvironment WebHostEnvironment, IHubContext<ProductHub> ProductHub)
        {
            this.UnitOFWork = UnitOFWork;
            this.WebHostEnvironment = WebHostEnvironment;
            this.ProductHub = ProductHub;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            List<Product> Products = UnitOFWork.ProductRepo.GetAll("Category").ToList();
            return View("Index", Products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(Product Product)
        {
            if (Product != null)
            {
                return View("Details", Product);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            ProductCategoryViewModel model = UnitOFWork.ProductRepo.GetProductWithCategories();
            return View("Create",model);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product Product)
        {
            
                if (ModelState.IsValid)
                {
                    if (Product.CategoryId == 0)
                    {
                        ModelState.AddModelError("", "Please Select Department");
                        ProductCategoryViewModel model = UnitOFWork.ProductRepo.GetProductWithCategories();
                        return View("Create", model);
                    }
                else
                    {
                        if (Product.Image != null)
                        {
                            string UploadPath = Path.Combine(WebHostEnvironment.WebRootPath, "Images", "Product");
                            string ImageName = Guid.NewGuid().ToString() + "_" + Product.Image.FileName;
                            string FilePath = Path.Combine(UploadPath, ImageName);
                            using (FileStream FileStream = new FileStream(FilePath, FileMode.Create))
                            {
                                Product.Image.CopyTo(FileStream);
                            }

                            Product.ImageUrl = ImageName;
                        }

                        UnitOFWork.ProductRepo.add(Product);
                        UnitOFWork.ProductRepo.save();
                        ProductHub.Clients.All.SendAsync("NewProductAdded", new {ID = Product.ID, Name = Product.Name, Image = Product.Image,
                         ImageUrl = Product.ImageUrl, Description = Product.Description,Category = Product.Category,
                            CategoryId = Product.CategoryId
                        });
                    return RedirectToAction(nameof(Index));
                    }


                }
                else
                {
                    ModelState.AddModelError("", "Please Enter Invalid Data");
                    ProductCategoryViewModel model = UnitOFWork.ProductRepo.GetProductWithCategories();
                    return View("Create", model);
                }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(Product product)
        {
            ProductCategoryViewModel model = UnitOFWork.ProductRepo.GetProductWithCategories();
            model.Product = product;
            return View("Edit",model);
        }

        //POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEdit(Product Product)
        {
            if (ModelState.IsValid && Product != null)
            {
                if (Product.Image != null)
                {
                    string UploadPath = Path.Combine(WebHostEnvironment.WebRootPath, "Images", "Product");
                    if(Product.ImageUrl!= null)
                    {
                        string ImageName = Product.ImageUrl;
                        string FilePath = Path.Combine(UploadPath, ImageName);

                        if (System.IO.File.Exists(FilePath))
                        {
                            try
                            {
                                System.IO.File.Delete(FilePath);
                            }
                            catch (Exception ex)
                            {
                                ModelState.AddModelError("", "Error deleting image: " + ex.Message);
                                return View(Product);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "File Not Found");
                            return View(Product);
                        }
                    }
                    else
                    {
                        string ImageName = Guid.NewGuid().ToString() + "_" + Product.Image.FileName;
                        string FilePath = Path.Combine(UploadPath, ImageName);
                        Product.ImageUrl = ImageName;
                        using (FileStream FileStream = new FileStream(FilePath, FileMode.Create))
                        {
                            Product.Image.CopyTo(FileStream);
                        }
                    } 
                }

                UnitOFWork.ProductRepo.update(Product);
                UnitOFWork.ProductRepo.save();

                return RedirectToAction(nameof(Index));
            }

            return View(Product);
        }



        [HttpGet]
        public ActionResult Delete(Product product) {

            return View("Delete", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Product Product = UnitOFWork.ProductRepo.Get(p => p.ID == id);
            if (Product != null)
            {
                UnitOFWork.ProductRepo.remove(Product);
                UnitOFWork.ProductRepo.save();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

        public ActionResult ViewProducts()
        {
            CategoryProductViewModel categoryProductViewModel = new CategoryProductViewModel();
            categoryProductViewModel.Categories = UnitOFWork.CategoryRepo.GetAll().ToList();
            categoryProductViewModel.Products = UnitOFWork.ProductRepo.GetAll().ToList();
            return View(categoryProductViewModel);
        }
    }
}
