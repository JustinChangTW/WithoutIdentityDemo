using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WithoutIdentityDemo.Extensions;
using WithoutIdentityDemo.Models;

namespace WithoutIdentityDemo.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(LoginModel data)
        {
            
            if (data.Badge == "Justin" && data.CipherCode == "1111111111")
            {
                //加密
                var encode = data.Badge.ToAesEncode();

                HttpContext.Response.Cookies.Append("auth_token",encode,
                    new CookieOptions
                    {
                      HttpOnly = true, //不可以被前端修改
                    }
                );
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(nameof(HomeController.Index), data);
        }

        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("auth_token"); //刪除cookie
            return RedirectToAction(nameof(AccountController.Index));
        }

    }
}
