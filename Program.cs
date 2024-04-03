using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using ToaPro.Models;
using ToaPro;
using System.Runtime.ConstrainedExecution;

// using ToaPro.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ToaPro") ?? throw new InvalidOperationException("Connection string 'ToaProContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddDbContext<ToaProContext>(options =>{
    options.UseNpgsql(builder.Configuration["ConnectionStrings:ToaPro"]);
});

//builder.Services.AddDataProtection();

builder.Services.AddDefaultIdentity<ToaProUser>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 12;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredUniqueChars = 2;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
}).AddRoles<IdentityRole>().AddEntityFrameworkStores<ToaProContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.IsEssential = true;
    options.LoginPath = "/Identity/Login";
    options.AccessDeniedPath = "/Identity/AccessDenied";
    options.LogoutPath = "/Identity/Logout";
    options.SlidingExpiration = true;
});

builder.Services.AddScoped<IIntexRepository, EFIntexRepository>();

var app = builder.Build();

// Seed user roles in case they don't exist
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    if (!await roleManager.RoleExistsAsync("Coordinator"))
    {
        await roleManager.CreateAsync(new IdentityRole("Coordinator"));
    }
    if (!await roleManager.RoleExistsAsync("Professor"))
    {
        await roleManager.CreateAsync(new IdentityRole("Professor"));
    }
    if (!await roleManager.RoleExistsAsync("TA"))
    {
        await roleManager.CreateAsync(new IdentityRole("TA"));
    }
    if (!await roleManager.RoleExistsAsync("Judge"))
    {
        await roleManager.CreateAsync(new IdentityRole("Judge"));
    }
    if (!await roleManager.RoleExistsAsync("Student"))
    {
        await roleManager.CreateAsync(new IdentityRole("Student"));
    }
}


//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
//else
//{
//    // Seed the database with testing data if in development mode
//    using (var scope = app.Services.CreateScope())
//    {
//        var context = scope.ServiceProvider.GetRequiredService<ToaProContext>();
//        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ToaProUser>>();
//        var dataSeeder = new DataSeeder(context, userManager);

//        bool userSeedSuccess = await dataSeeder.SeedUsers();

//        await dataSeeder.SeedData();
//    }
//}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
