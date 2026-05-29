using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab5.Models;

namespace Lab5.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // Chỉ Admin mới được quản lý thành viên
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Dữ liệu mô hình truyền ra View
        public class UserViewModel
        {
            public string Id { get; set; } = string.Empty;
            public string UserName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string Address { get; set; } = string.Empty;
            public IList<string> Roles { get; set; } = new List<string>();
        }

        // GET: Admin/User
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName ?? string.Empty,
                    Email = user.Email ?? string.Empty,
                    FullName = user.FullName,
                    Address = user.Address,
                    Roles = roles
                });
            }

            return View(userViewModels);
        }

        // POST: Admin/User/ToggleAdmin/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Tránh việc tự hạ quyền của chính mình
            if (user.UserName == User.Identity?.Name)
            {
                TempData["ErrorMessage"] = "Bạn không thể tự tước quyền Admin của chính mình!";
                return RedirectToAction(nameof(Index));
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (isAdmin)
            {
                // Hạ quyền thành Member
                await _userManager.RemoveFromRoleAsync(user, "Admin");
                if (!await _userManager.IsInRoleAsync(user, "Member"))
                {
                    await _userManager.AddToRoleAsync(user, "Member");
                }
                TempData["SuccessMessage"] = $"Đã thu hồi quyền Admin của người dùng {user.UserName}.";
            }
            else
            {
                // Thăng quyền thành Admin
                if (await _userManager.IsInRoleAsync(user, "Member"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "Member");
                }
                await _userManager.AddToRoleAsync(user, "Admin");
                TempData["SuccessMessage"] = $"Đã cấp quyền Admin cho người dùng {user.UserName} thành công!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
