using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OutOfOffice.Helpers;
using OutOfOffice.Managers;
using OutOfOffice.Models;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OutOfOffice.Controllers
{
    public class HomeController : Controller
    {
        private readonly IManager _manager;

        public HomeController(IManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["UserName"] = User.Identity.Name;
                ViewData["Role"] = User.FindFirstValue(ClaimTypes.Role);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            var user = await _manager.GetUserByEmailAsync(model.Email);
            if (user != null && Sha256Helper.ComputeHash(model.Password) == user.Password)
            {
                await HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Employee.FullName),
                    new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Employee.Position.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                if (returnUrl != null)
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Incorrect email or password");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
