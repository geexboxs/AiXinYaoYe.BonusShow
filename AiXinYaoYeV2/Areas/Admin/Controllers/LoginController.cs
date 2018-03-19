using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using AiXinYaoYeV2.Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AiXinYaoYeV2.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    public class LoginController: Controller
    {
        private MyDbContext dbContext;

        public LoginController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ActionResult Login() {
            return View();
        }
        [HttpPost]
        public ActionResult LoginPost(string username, string password)
        {
            if (this.dbContext.Admins.Any(x=>x.UserName == username && x.Password == password))
            {
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "admin"),
                }, CookieAuthenticationDefaults.AuthenticationScheme));
                Request.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,claimsPrincipal,new AuthenticationProperties()
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7),
                    IsPersistent = true,
                    IssuedUtc = DateTimeOffset.UtcNow,
                });
                return RedirectToAction("Index", "BonusProduct");
            }

            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {


            var authContext = Request.HttpContext.AuthenticateAsync().Result;
            Request.HttpContext.SignInAsync(authContext.Principal);
            Request.HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}
