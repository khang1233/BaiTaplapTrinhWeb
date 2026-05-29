using Lab5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Rất quan trọng để hỗ trợ Identity Razor Pages

// Đăng ký MyDbContext (Cũ của bạn)
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký BookDbContext cho bài ASM5
builder.Services.AddDbContext<BookDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký dịch vụ Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<BookDbContext>()
    .AddDefaultTokenProviders();

// Tối giản chính sách mật khẩu để cho phép admin/admin và member/member
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3; // Mật khẩu từ 3 ký tự trở lên
    options.Password.RequiredUniqueChars = 0;
});

// Cấu hình đường dẫn Cookie mặc định
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

var app = builder.Build();

// Tự động Seed dữ liệu Roles & Tài khoản admin/admin, member/member khi khởi chạy
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<BookDbContext>();
        
        // Chạy migration tự động
        context.Database.Migrate();

        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        // 1. Tạo sẵn hai vai trò Admin và Member
        string[] roleNames = { "Admin", "Member" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // 2. Tạo và Seed tài khoản Admin (admin / admin)
        var adminUser = await userManager.FindByNameAsync("admin");
        if (adminUser == null)
        {
            var user = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@qlbanhang.com",
                FullName = "Quản trị viên Hệ thống",
                Address = "Hà Nội, Việt Nam",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, "admin");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        // 3. Tạo và Seed tài khoản Member (member / member)
        var memberUser = await userManager.FindByNameAsync("member");
        if (memberUser == null)
        {
            var user = new ApplicationUser
            {
                UserName = "member",
                Email = "member@qlbanhang.com",
                FullName = "Khách hàng Thành viên",
                Address = "TP. Hồ Chí Minh, Việt Nam",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, "member");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Member");
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Đã xảy ra lỗi trong quá trình Seeding dữ liệu Roles và Users.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Thêm UseAuthentication vào đúng vị trí trước UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

// Định tuyến cho Area Admin
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Định tuyến mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Định tuyến cho các trang Razor Pages của Identity
app.MapRazorPages();

app.Run();