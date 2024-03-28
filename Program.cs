using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ToaPro.Models;
using ToaPro;

// using ToaPro.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ToaProContext>(options =>{
    options.UseNpgsql(builder.Configuration["ConnectionStrings:ToaPro"]);
});
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 12;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredUniqueChars = 2;
    options.Stores.ProtectPersonalData = true;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = true;
    options.SignIn.RequireConfirmedEmail = true;
}).AddEntityFrameworkStores<ToaProContext>();
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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
