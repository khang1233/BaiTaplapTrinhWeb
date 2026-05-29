using Microsoft.AspNetCore.Mvc;
using BaiTapBuoi6.Repository;

namespace BaiTapBuoi6.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        // GET: Product
        // Xem danh sách sản phẩm và lọc theo danh mục
        public async Task<IActionResult> Index(int? categoryId)
        {
            var products = await _productRepository.GetAllAsync();
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value).ToList();
                ViewBag.CurrentCategoryId = categoryId.Value;
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = categories;
            return View(products);
        }

        // GET: Product/Details/5
        // Xem chi tiết sản phẩm công cộng
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
