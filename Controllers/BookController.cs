using Lab5.Models;
using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lab5.Controllers
{
    public class BookController : Controller
    {
        private readonly BookDbContext _context;

        // Nhận bối cảnh dữ liệu đã đăng ký từ hệ thống
        public BookController(BookDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách sách kèm thông tin chủ đề để hiển thị ra View
        public async Task<IActionResult> Index(int? categoryId)
        {
            var query = _context.Books.Include(b => b.Category).AsQueryable();
            if (categoryId.HasValue)
            {
                query = query.Where(b => b.CategoryId == categoryId.Value);
            }
            var books = await query.ToListAsync();
            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books.Include(b => b.Category).FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
    }
}