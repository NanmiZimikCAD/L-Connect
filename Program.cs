using Microsoft.EntityFrameworkCore;
using L_Connect.Data;
using L_Connect.Models.Domain;
using Microsoft.AspNetCore.Identity;  // Added this for IPasswordHasher
using Microsoft.AspNetCore.Authentication.Cookies;// Added this for Authentication
using L_Connect.Services.Interfaces;
using L_Connect.Services.Implementations;

// - Database connection
// - Authentication services
// - Cookie settings
// - Password hasher service

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register application services
builder.Services.AddScoped<IShipmentService, ShipmentService>();
// Register services for dependency injection
builder.Services.AddScoped<L_Connect.Services.Interfaces.IShipmentService, L_Connect.Services.Implementations.ShipmentService>();
builder.Services.AddScoped(typeof(L_Connect.Services.Interfaces.IUserService), typeof(L_Connect.Services.Implementations.UserService));

// Add Cookie Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(24);
});

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

//phase 3: Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

//order matters here
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
