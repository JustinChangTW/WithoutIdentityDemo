using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WithoutIdentityDemo.Models;

namespace WithoutIdentityDemo.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(LoginModel login)
        {
            if(login.Badge=="Justin" && login.CipherCode == "1234567890")
            {
                //當登入成功後，將使用者資訊存入Session中
                HttpContext.Session.SetString("Badge", login.Badge);
                HttpContext.Session.SetString("Role", "administraotr");
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(nameof(AccountController.Index), login);
        }
        public IActionResult Logout()
        {
            var badge = HttpContext.Session.GetString("Badge");
            var role = HttpContext.Session.GetString("Role");

            HttpContext.Session.Clear();

            badge = HttpContext.Session.GetString("Badge");
            role = HttpContext.Session.GetString("Role");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

    }
}
