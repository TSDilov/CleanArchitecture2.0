using Hr.LeaveManagement.MVC.Contracts;
using Hr.LeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hr.LeaveManagement.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAuthenticationService authService;

        public UsersController(IAuthenticationService authService)
        {
            this.authService = authService;
        }

        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (ModelState.IsValid) 
            {
                var returnUrl = Url.Content("~/");
                var isLoggedIn = await this.authService.Authenticate(login.Email, login.Password);
                if (isLoggedIn)
                {
                    return LocalRedirect(returnUrl);
                }
            }

            ModelState.AddModelError("", "Log In Attempt Failed. Please try again.");
            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registration)
        {
            try
            {
                var returnUrl = Url.Content("~/");
                if (ModelState.IsValid)
                {            
                    await this.authService.Register(registration.FirstName,
                        registration.LastName, registration.UserName, registration.Email, registration.Password);                 
                }

                return LocalRedirect(returnUrl);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(registration);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            await this.authService.Logout();
            return LocalRedirect(returnUrl);
        }
    }
}
