using Eagles_Website.Models;
using Eagles_Website.Repository.IRepository;
using Eagles_Website.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eagles_Website.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOFWork unitOFWork;

        public OrderController(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
        }
        // GET: OrderController
        public ActionResult Index()
        {
            List<Order> Orders = unitOFWork.OrderRepo.GetAll("ApplicationUser,OrderDetails").ToList();
            return View("Index",Orders);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            Order Order = unitOFWork.OrderRepo.Get(O => O.ID == id, "ApplicationUser,OrderDetails");
            List<OrderDetails> OrderDetails = Order.OrderDetails.ToList();
            return View("Details", OrderDetails);
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
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
        [HttpGet]
        public IActionResult CreateOrder(Cart Cart)
        {
            Cart CartDB = unitOFWork.CartRepo.Get(C => C.ID == Cart.ID,"CartItems");
            OrderViewModel OrderViewModel = new OrderViewModel();
            OrderViewModel.CartID = CartDB.ID;
            OrderViewModel.TotalAmount = CartDB.CartItems.Count;
            OrderViewModel.OrderDate = DateTime.Now;
            return View("CreateOrder",OrderViewModel);
        }

        public IActionResult Pay()
        {
            return View();
        }
    }
}
