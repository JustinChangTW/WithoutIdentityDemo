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

        public IActionResult Login(LoginModel data)
        {
            if (data.Badge == "Justin" && data.CipherCode == "1111111111")
            {
                //to do append cookie

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(nameof(HomeController.Index), data);
        }

        public IActionResult Logout()
        {
            //to do clean cookie

            return View();
        }

    }
}
