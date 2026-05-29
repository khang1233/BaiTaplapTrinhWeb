using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BaiTapBuoi6.Models;
using BaiTapBuoi6.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Đăng ký Razor Pages phục vụ cho Identity UI mặc định

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký dịch vụ Identity sử dụng ApplicationUser kế thừa
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Thiết lập cấu hình Identity
    options.SignIn.RequireConfirmedAccount = false; // Tắt yêu cầu xác nhận tài khoản qua email để thuận tiện test
    options.Password.RequireDigit = false;          // Tắt yêu cầu ký tự số
    options.Password.RequiredLength = 3;            // Độ dài mật khẩu tối thiểu để hỗ trợ mật khẩu "admin" và "member"
    options.Password.RequireNonAlphanumeric = false; // Tắt yêu cầu ký tự đặc biệt
    options.Password.RequireUppercase = false;      // Tắt yêu cầu chữ hoa
    options.Password.RequireLowercase = false;      // Tắt yêu cầu chữ thường
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddDefaultUI();

// Đăng ký dịch vụ Repository (Scoped Lifetime)
builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();
builder.Services.AddScoped<IProductRepository, EFProductRepository>();

var app = builder.Build();

// Tự động Seed Roles ("Admin", "Member") và tài khoản mặc định
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        // Seed Roles
        string[] roleNames = { "Admin", "Member" };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Seed Admin User (admin/admin)
        var existingAdmin = await userManager.FindByNameAsync("admin");
        if (existingAdmin != null)
        {
            await userManager.DeleteAsync(existingAdmin);
        }
        
        var admin = new ApplicationUser
        {
            UserName = "admin",
            Email = "admin@qlbanhang.com",
            FullName = "System Administrator",
            Address = "Hanoi, Vietnam",
            EmailConfirmed = true
        };
        var createAdmin = await userManager.CreateAsync(admin, "admin");
        if (createAdmin.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        // Seed Member User (member/member)
        var existingMember = await userManager.FindByNameAsync("member");
        if (existingMember != null)
        {
            await userManager.DeleteAsync(existingMember);
        }

        var member = new ApplicationUser
        {
            UserName = "member",
            Email = "member@qlbanhang.com",
            FullName = "Regular Member",
            Address = "Saigon, Vietnam",
            EmailConfirmed = true
        };
        var createMember = await userManager.CreateAsync(member, "member");
        if (createMember.Succeeded)
        {
            await userManager.AddToRoleAsync(member, "Member");
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Lỗi xảy ra trong quá trình seeding cơ sở dữ liệu.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Thêm Middleware xác thực (Authentication) trước Middleware phân quyền (Authorization)
app.UseAuthentication();
app.UseAuthorization();

// Route Area cho Admin (Bắt buộc map trước route default)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Map các trang Razor Pages của Identity UI mặc định

app.Run();
