using Eagles_Website.Models;
using Eagles_Website.Repository;
using Eagles_Website.Repository.IRepository;
using Eagles_Website.ViewModels.LoginRegisterViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Eagles_Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUnitOFWork unitOfWork;
        public AccountController(IUnitOFWork unitOfWork,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveRegister(RegisteredCustomer NewCustomer)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                UserName = NewCustomer.UserName,
                Email = NewCustomer.Email,
                PhoneNumber = NewCustomer.PhoneNumber,
                ZipCode = NewCustomer.ZipCode,
                Address = NewCustomer.Address,
                PasswordHash = NewCustomer.Password
                };
                IdentityResult result = await userManager.CreateAsync(applicationUser, applicationUser.PasswordHash);
                if (result.Succeeded)
                {
                    
                    await userManager.AddToRoleAsync(applicationUser, "Admin");
                    await signInManager.SignInAsync(applicationUser, false);
                    unitOfWork.CartRepo.add(new Cart()
                    {
                        UserID = applicationUser.Id,
                        TotalPrice = 0
                    });
                    unitOfWork.CartRepo.save();
                    return RedirectToAction("Index", "Home");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("Register", NewCustomer);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveLogin(LoggedInCustomer LoggedInCustomer)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel =
                    await userManager.FindByNameAsync(LoggedInCustomer.UserName);
                if (userModel != null)
                {
                    bool found = await userManager.CheckPasswordAsync(userModel, LoggedInCustomer.Password);
                    if (found == true)
                    {
                        await signInManager.SignInAsync(userModel,false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Invalid Credentials Please try again");
            }
            return View("Login", LoggedInCustomer);
        }
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
