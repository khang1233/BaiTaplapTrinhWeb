using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BaiTapBuoi6.Models;
using BaiTapBuoi6.Repository;

namespace BaiTapBuoi6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            // Lấy ra tối đa 6 sản phẩm nổi bật hiển thị ở trang chủ
            var products = await _productRepository.GetAllAsync();
            var featuredProducts = products.Take(6).ToList();
            return View(featuredProducts);
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
