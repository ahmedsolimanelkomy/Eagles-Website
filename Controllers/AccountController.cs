using Eagles_Website.Models;
using Eagles_Website.Repository.IRepository;
using Eagles_Website.ViewModels.LoginRegisterCustomerViewModel;
using Eagles_Website.ViewModels.LoginRegisterViewModel;
using Microsoft.AspNetCore.Authorization;
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
                    
                    await userManager.AddToRoleAsync(applicationUser, "Customer");
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
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.StreetAddress,userModel.Address));
                        claims.Add(new Claim(ClaimTypes.MobilePhone, userModel.PhoneNumber));
                        //await signInManager.SignInAsync(userModel, false);
                        await signInManager.SignInWithClaimsAsync(userModel, false, claims);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Invalid Credentials Please try again");
            }
            return View("Login", LoggedInCustomer);
        }
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            //var x = HttpContext.User.Claims.Where(c=>c.Type==ClaimTypes.MobilePhone).FirstOrDefault().Value;
            RequiredToEditCustomer requiredToEditCustomer = new RequiredToEditCustomer();
            ApplicationUser usermodel = await userManager.FindByNameAsync(HttpContext.User.Claims.Where((c) => c.Type == ClaimTypes.Name).FirstOrDefault().Value);
            requiredToEditCustomer.FullName = usermodel.UserName;
            requiredToEditCustomer.Address = usermodel.Address;
            requiredToEditCustomer.NewEmail = usermodel.Email;
            requiredToEditCustomer.PhoneNumber = usermodel.PhoneNumber;
            return View(requiredToEditCustomer);
        }
        [HttpPost]
        public async Task<IActionResult> SaveEditAsync(RequiredToEditCustomer requiredToEditCustomer)
        {
            if (ModelState.IsValid)
            {
                string OldName = HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault().Value;
                ApplicationUser userModel = await userManager.FindByNameAsync(OldName);
                if (userModel != null)
                {
                    bool found = await userManager.CheckPasswordAsync(userModel, requiredToEditCustomer.OldPassword);
                    if (found == true)
                    {
                        userModel.Email = requiredToEditCustomer.NewEmail;
                        userModel.PhoneNumber = requiredToEditCustomer.PhoneNumber;
                        userModel.Address = requiredToEditCustomer.Address;
                        userModel.UserName = requiredToEditCustomer.FullName;
                        await userManager.ChangePasswordAsync(userModel, requiredToEditCustomer.OldPassword, requiredToEditCustomer.NewPassword);
                        await userManager.UpdateAsync(userModel);
                        LoggedInCustomer loggedInCustomer = new LoggedInCustomer();
                        loggedInCustomer.UserName = requiredToEditCustomer.FullName;
                        loggedInCustomer.Password = requiredToEditCustomer.OldPassword;
                        return await SaveLogin(loggedInCustomer);
                    }
                }
                ModelState.AddModelError("", "Invalid Credentials Please try again");

            }
            return View("Edit", requiredToEditCustomer);
        }
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
