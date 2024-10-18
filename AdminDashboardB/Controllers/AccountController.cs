using AdminDashboardB.Models;
using ApplicationB.Services_B;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsB.Authentication_and_Authorization_B;

namespace AdminDashboardB.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUserB> _userManager;
        private readonly SignInManager<ApplicationUserB> _signInManager;
        private readonly IUserService _userService;


        public AccountController(UserManager<ApplicationUserB> userManager, SignInManager<ApplicationUserB> signInManager,IUserService userservice)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userservice;
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
        public async Task<IActionResult> Register(RegisterVM model)
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
        public async Task<IActionResult> Login(LoginVm model)
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

                    else if (user.UserType == "User")
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


        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return View("users",users);
        }

        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View("GetOne",user); 
        }

        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return View(user); // Return user for view
        }

        [HttpGet("{id}/edit")]
        public async Task<IActionResult> UpdateUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View("Update",user); // Return user for editing
        }

        [HttpPost("{id}/edit")]
        public async Task<IActionResult> UpdateUser(ApplicationUserB user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByIdAsync(user.Id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                // Update user properties
                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Address = user.Address;
                existingUser.City = user.City;
                existingUser.Country = user.Country;
                existingUser.PostalCode = user.PostalCode;
                existingUser.UserType = user.UserType;

                var result = await _userManager.UpdateAsync(existingUser);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetAllUsers"); // Redirect to user list after update
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(user); 
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetAllUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return RedirectToAction("GetAllUsers");
        }
    }






    //public async Task<IActionResult> GetAllUsers()
    //{
    //    var users = await _userService.GetAllAppUsersAsync();
    //    return Ok(users);
    //}





    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetUserById(string id)
    //{
    //    var user = await _userService.GetAppUserByIdAsync(id);
    //    if (user == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(user);
    //}

    //[HttpGet("email/{email}")]
    //public async Task<IActionResult> GetUserByEmail(string email)
    //{
    //    var user = await _userService.GetAppUserByEmailAsync(email);
    //    if (user == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(user);
    //}

    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDto userDto)
    //{
    //    if (id != userDto.Id)
    //    {
    //        return BadRequest();
    //    }

    //    await _userService.UpdateAppUserAsync(userDto);
    //    return NoContent();
    //}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteUser(string id)
    //{
    //    await _userService.DeleteAppUserAsync(id);
    //    return NoContent();
    //}
}
 



