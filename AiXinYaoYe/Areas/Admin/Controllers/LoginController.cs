using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using AiXinYaoYe.Database;

namespace AiXinYaoYe.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class LoginController:Controller
    {
        public ActionResult Login() {
            return View();
        }
        [HttpPost]
        public ActionResult LoginPost(string username, string password)
        {
           

            var dbContext = new MyDbContext();
            if (dbContext.Admins.Any(x=>x.UserName == username && x.Password == password))
            {
                var authContext = Request.GetOwinContext();
                var authManager = authContext.Authentication;
                var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.NameIdentifier, username),
                    },
                    "ApplicationCookie");
                authManager.SignIn(identity);
                return RedirectToAction("Index", "BonusProduct");

            }

            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {


            var authContext = Request.GetOwinContext();
            var authManager = authContext.Authentication;
            authManager.SignOut();

            return RedirectToAction("Login");
        }
    }
}
