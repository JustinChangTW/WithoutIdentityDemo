using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        public IActionResult Login(LoginModel data)
        {
            
            if (data.Badge == "Justin" && data.CipherCode == "1111111111")
            {

                //加密
                var encode = "";
                var aes = new AesCryptoServiceProvider();
                SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

                byte[] key = sha256.ComputeHash(Encoding.ASCII.GetBytes("12345678"));
                byte[] iv = md5.ComputeHash(Encoding.ASCII.GetBytes("12345678"));
                aes.Key = key;
                aes.IV = iv;
                var dataArray = Encoding.UTF8.GetBytes(data.Badge);
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataArray, 0, dataArray.Length);
                    cs.FlushFinalBlock();
                    encode = Convert.ToBase64String(dataArray.ToArray());
                }


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
