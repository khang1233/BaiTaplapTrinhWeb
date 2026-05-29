using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Lab5.Models;

namespace Lab5.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public string? ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
            [StringLength(100, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự.", MinimumLength = 3)]
            [Display(Name = "Tên đăng nhập")]
            public string UserName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Email là bắt buộc")]
            [EmailAddress(ErrorMessage = "Email không hợp lệ")]
            [Display(Name = "Email")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Họ và tên là bắt buộc")]
            [StringLength(100, ErrorMessage = "{0} phải dài từ {2} đến {1} ký tự.", MinimumLength = 2)]
            [Display(Name = "Họ và tên")]
            public string FullName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
            [StringLength(200, ErrorMessage = "{0} không được vượt quá {1} ký tự.")]
            [Display(Name = "Địa chỉ")]
            public string Address { get; set; } = string.Empty;

            [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
            [StringLength(100, ErrorMessage = "{0} phải dài ít nhất {2} ký tự.", MinimumLength = 3)]
            [DataType(DataType.Password)]
            [Display(Name = "Mật khẩu")]
            public string Password { get; set; } = string.Empty;

            [DataType(DataType.Password)]
            [Display(Name = "Xác nhận mật khẩu")]
            [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
            public string ConfirmPassword { get; set; } = string.Empty;
        }

        public void OnGet(string? returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser 
                { 
                    UserName = Input.UserName, 
                    Email = Input.Email,
                    FullName = Input.FullName,
                    Address = Input.Address
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Người dùng mới đã tạo tài khoản.");

                    // Tự động gán quyền Member cho tài khoản mới đăng ký
                    if (!await _roleManager.RoleExistsAsync("Member"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Member"));
                    }
                    await _userManager.AddToRoleAsync(user, "Member");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
