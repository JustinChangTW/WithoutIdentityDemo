using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WithoutIdentityDemo.Extensions;
using WithoutIdentityDemo.Models;

namespace WithoutIdentityDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HttpContext.Response.Cookies.Append("Key", "Value");
            return View();
        }

        public IActionResult Privacy()
        {
            var value = HttpContext.Request.Cookies.SingleOrDefault(x => x.Key == "auth_token").Value;
            var decode = value.ToDecode();

            if (decode != "Justin")
                return RedirectToAction(nameof(AccountController.Index));

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
