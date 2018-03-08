using System.Linq;
using System.Web.Mvc;
using AiXinYaoYe.Database;

namespace AiXinYaoYe.Areas.Admin.Controllers
{
    public class LoginController:Controller
    {
        public ActionResult Login() {
            return View();
        }
        [HttpPost]
        public ActionResult LoginPost(string username, string password)
        {
            var dbContext = new MyEntities();
            if (dbContext.Admins.Any(x=>x.UserName == username && x.Password == password))
            {
                HttpContext.Session.Add($"{username}{HttpContext.Request.UserHostAddress}","");
                return RedirectToAction("List", "BonusProduct");

            }

            return RedirectToAction("Login");
        }
    }
}
