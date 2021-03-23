using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WithoutIdentityDemo.Models;
using WithoutIdentityDemo.Extensions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace WithoutIdentityDemo.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login(LoginModel login)
        {
            if(login.Badge=="Justin" && login.CipherCode == "1234567890")
            {
                //當登入成功後，將使用者資訊存入Session中
                HttpContext.Session.Set(new UserInfo()
                {
                    Name = login.Badge,
                    Role = "administrator"
                });

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(nameof(AccountController.Index), login);
        }
        public IActionResult Logout()
        {
            var userinfo = HttpContext.Session.Get<UserInfo>();

            HttpContext.Session.Clear();

            userinfo = HttpContext.Session.Get<UserInfo>();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

    }
}
