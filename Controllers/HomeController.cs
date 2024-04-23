using Eagles_Website.Models;
using Eagles_Website.Repository;
using Eagles_Website.Repository.IRepository;
using Eagles_Website.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Eagles_Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOFWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOFWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.ProductRepo.GetAll().ToList();
            return View("Index", products);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Details(int id)
        {
            CartItem CartItem = new CartItem()
            {
                Product = _unitOfWork.ProductRepo.Get(c => c.ID == id, "Category"),
                ProductID = id
            };
            return View(CartItem);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(CartItem cartitem)
        {

            int userid = int.Parse(User.Claims.FirstOrDefault(s => s.Type == ClaimTypes.NameIdentifier)?.Value);
            Cart cart = _unitOfWork.CartRepo.Get(c => c.ApplicationUser.Id == userid);

            CartItem c = _unitOfWork.CartItemRepo.Get(c => c.ProductID == cartitem.ProductID && c.CartID == cart.ID);
            if (c != null)
            {
                c.Quantity += cartitem.Quantity;
                c.SubTotal = c.UnitPrice * c.Quantity;
                _unitOfWork.CartItemRepo.update(c);
            }
            else
            {
                cartitem.Id = 0;
                cartitem.CartID = cart.ID;
                cartitem.UnitPrice = _unitOfWork.ProductRepo.Get(p => p.ID == cartitem.ProductID).Price;
                cartitem.SubTotal += cartitem.UnitPrice * cartitem.Quantity;
                _unitOfWork.CartItemRepo.add(cartitem);
            }

            _unitOfWork.CartItemRepo.save();
            return RedirectToAction("Index","Cart");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
