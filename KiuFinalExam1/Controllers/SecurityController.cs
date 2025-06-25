using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace KiuFinalExam1.Controllers
{
    public class SecurityController : Controller
    {

        static Dictionary<string, string> users = new Dictionary<string, string>
        {
            { "admin", "1234" },
            { "user1", "password}" },
            { "user2", "mypassword" }
            };

        public IActionResult Login()
        {
            return View();
        }
        //es post metodia mixvdebi albat mansplaining da temebi
        [HttpPost]
        public IActionResult Login(String UID, String PWD)
        {
            // gamocdaze ikneba dict mocemuli da ase awvdi krch
            if (users.ContainsKey(UID) && users[UID] == PWD)
            {
                
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, UID) };
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimIdentity)).Wait();
                return RedirectToAction("Index", "Home");
                //Redirect shouldnt be used without a return type
                //Now we are redirected to the Home controller's Index action

            }
            else
            {
                // ak shegidzlia prst ViewBag.Msg mara idk 
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }

        }
        public IActionResult Logout() {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Login", "Security");
        }


    }
}
