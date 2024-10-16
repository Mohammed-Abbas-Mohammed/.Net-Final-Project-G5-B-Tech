using AdminDashboardB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelsB.Authentication_and_Authorization_B;

namespace AdminDashboardB.Controllers
{
    public class AccountController : Controller
    {
                private readonly UserManager<ApplicationUserB> _userManager;
        private readonly SignInManager<ApplicationUserB> _signInManager;

        public AccountController(UserManager<ApplicationUserB> userManager, SignInManager<ApplicationUserB> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
       [HttpPost]
public async Task<IActionResult> Register(RegisterVMcs model)
{
    if (ModelState.IsValid)
    {
        var user = new ApplicationUserB
        {
            UserName = model.UserName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Address = model.Address,
            City = model.City,
            Country = model.Country,
            PostalCode = model.PostalCode,
            UserType = model.UserType, // This captures Admin or User from the form
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            if (model.UserType == "Admin")
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home"); // Redirect to homepage after successful registration
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }

    return View(model); // Re-display the form if there are validation errors
}

        public IActionResult Login()
{
    return View();
}

[HttpPost]
        public async Task<IActionResult> Login(LoginVMcs model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (user.UserType == "Admin")
                    {
                        return RedirectToAction("Index", "Home"); // Admin is redirected to dashboard
                    }

                    else if (user.UserType=="User")
                    {
                        return RedirectToAction("Index", "HomeSite"); // User is redirected to home site page
                    }

                    return RedirectToAction("Register", "Account"); // Redirect to homepage after successful login
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "This account has been locked out due to multiple failed login attempts.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View(model); // Re-display the form if there are validation errors
        }


        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync(); // Sign the user out

            return RedirectToAction("Login", "Account"); // Redirect to homepage after signing out
        }
    }

   
}
