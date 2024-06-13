using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
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
            if(user!=null && ComputeSha256Hash(model.Password) == user.Password)
            {
                await HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Employee.FullName),
                        new Claim(ClaimTypes.Role, user.Employee.Position.ToString()),
                    };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));
            }

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
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
