using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab5.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Tự động chuyển hướng đến cửa hàng sách tuyệt đẹp
            return RedirectToAction("Index", "Book");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
