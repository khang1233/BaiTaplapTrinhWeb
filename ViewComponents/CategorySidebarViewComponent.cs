using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lab5.ViewComponents
{
    public class CategorySidebarViewComponent : ViewComponent
    {
        private readonly BookDbContext _context;

        public CategorySidebarViewComponent(BookDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories.Include(c => c.Books).ToListAsync();
            return View(categories);
        }
    }
}
