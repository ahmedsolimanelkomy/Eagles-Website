using Eagles_Website.Models;
using Eagles_Website.Repository.IRepository;
using Eagles_Website.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Eagles_Website.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOFWork unitOFWork;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public OrderController(IUnitOFWork unitOFWork, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.unitOFWork = unitOFWork;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        // GET: OrderController
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            List<Order> Orders = unitOFWork.OrderRepo.GetAll("ApplicationUser,OrderDetails").ToList();
            return View("Index",Orders);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult OrderDetails(Order Order)
        {
            return View(Order);
        }
        // GET: OrderController/Delete/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Order Order)
        {
            return View(Order);
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Order? Order = unitOFWork.OrderRepo.Get(O => O.ID == id);
            unitOFWork.OrderRepo.remove(Order);
            unitOFWork.OrderRepo.save();
            return RedirectToAction("Index");
        }
        [Authorize(Roles ="Admin")]
        public ActionResult Edit(Order Order)
        {
            return View(Order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            Order? Order = unitOFWork.OrderRepo.Get(O => O.ID == id);
            unitOFWork.OrderRepo.update(Order);
            unitOFWork.OrderRepo.save();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> CreateOrder(Cart Cart)
        {
            Cart CartDB = unitOFWork.CartRepo.Get(C => C.ID == Cart.ID,"CartItems");
            OrderViewModel OrderViewModel = new OrderViewModel();
            OrderViewModel.CartID = CartDB.ID;
            OrderViewModel.TotalAmount = CartDB.CartItems.Count;
            OrderViewModel.OrderDate = DateTime.Now;

            Order order = new Order();
            ApplicationUser? currentUser = await userManager.GetUserAsync(User);
            order.UserID = currentUser?.Id;
            order.OrderDate = OrderViewModel.OrderDate;
            order.TotalAmount = OrderViewModel.TotalAmount;
            //
            List<OrderDetails> orderDetails = new List<OrderDetails>();
            OrderDetails OrderD = new OrderDetails();
            //OrderD.Order = order;
            //OrderD.OrderId = order.ID;
            //OrderD.Quantity = CartDB.CartItems.Count;
            //OrderD.SubTotal = (int)CartDB.TotalPrice;
            //OrderD.UnitPrice = (int)(CartDB.TotalPrice / CartDB.CartItems.Count);
            //OrderViewModel.OrderDetails = OrderD;
            //orderDetails.Add(OrderD);
            //order.OrderDetails = orderDetails;

            //unitOFWork.OrderDetailRepo.add(OrderD);
            //unitOFWork.OrderDetailRepo.save();
            unitOFWork.OrderRepo.add(order);
            unitOFWork.OrderRepo.save();

            List<CartItem> CartItems = unitOFWork.CartItemRepo.GetList(C=>C.CartID == CartDB.ID).ToList();
            unitOFWork.CartItemRepo.removeRange(CartItems);
            unitOFWork.CartItemRepo.save();

            return View("CreateOrder",OrderViewModel);
        }

        // GET: OrderController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            string UserName = User.Identity.Name;
            ApplicationUser? currentUser = await userManager.GetUserAsync(User);
            IEnumerable<Order> Orders = unitOFWork.OrderRepo.GetList(O => O.UserID == currentUser.Id,"OrderDetails");
            return View("Details", Orders);
        }

        public IActionResult Pay()
        {
            return View();
        }

        public IActionResult ProductDetailsInformation(Order? Order)
        {
            OrderDetails? OrderDetails = unitOFWork.OrderDetailRepo.Get(OD => OD.OrderId == Order.ID);
            return View(OrderDetails);
        }
    }
}
