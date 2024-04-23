using Eagles_Website.Models;
using Eagles_Website.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Eagles_Website.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOFWork _unitOfWork;
        public CartController(IUnitOFWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public IActionResult Index()
        {
            int userid = int.Parse(User.Claims.FirstOrDefault(s => s.Type == ClaimTypes.NameIdentifier)?.Value);
            Cart? cart = _unitOfWork.CartRepo.GetCartwithincludes(c => c.ApplicationUser.Id == userid);
            _unitOfWork.CartRepo.handletotal(cart);
            return View(cart);
        }
        public IActionResult plus(int cartitemid)
        {
            int userid = int.Parse(User.Claims.FirstOrDefault(s => s.Type == ClaimTypes.NameIdentifier)?.Value);
            Cart cart = _unitOfWork.CartRepo.Get(c => c.ApplicationUser.Id == userid);

            CartItem c = _unitOfWork.CartItemRepo.Get(c => c.Id == cartitemid && c.CartID == cart.ID);
            c.Quantity += 1;
            c.SubTotal = c.UnitPrice * c.Quantity;
            _unitOfWork.CartRepo.save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult minus(int cartitemid)
        {

            int userid = int.Parse(User.Claims.FirstOrDefault(s => s.Type == ClaimTypes.NameIdentifier)?.Value);
            Cart cart = _unitOfWork.CartRepo.Get(c => c.ApplicationUser.Id == userid);

            CartItem c = _unitOfWork.CartItemRepo.Get(c => c.Id == cartitemid && c.CartID == cart.ID);
            if (c.Quantity <= 1)
            {
                _unitOfWork.CartItemRepo.remove(c);
            }
            else
            {
                c.Quantity -= 1;
                c.SubTotal = c.UnitPrice * c.Quantity;


            }
            _unitOfWork.CartRepo.save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult delete(int cartitemid)
        {

            int userid = int.Parse(User.Claims.FirstOrDefault(s => s.Type == ClaimTypes.NameIdentifier)?.Value);
            Cart cart = _unitOfWork.CartRepo.Get(c => c.ApplicationUser.Id == userid);

            CartItem c = _unitOfWork.CartItemRepo.Get(c => c.Id == cartitemid && c.CartID == cart.ID);
            _unitOfWork.CartItemRepo.remove(c);
            _unitOfWork.CartRepo.save();
            return RedirectToAction(nameof(Index));

        }
    }
}
