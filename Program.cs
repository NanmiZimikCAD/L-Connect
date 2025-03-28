using Microsoft.EntityFrameworkCore;
using L_Connect.Data;
using L_Connect.Models.Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using L_Connect.Services.Interfaces;
using L_Connect.Services.Implementations;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add controllers with views
builder.Services.AddControllersWithViews();

// Register application services
builder.Services.AddScoped<IShipmentService, ShipmentService>();
builder.Services.AddScoped<IUserService, UserService>();

// Add DbContext (using MySQL)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// Register PasswordHasher for User so IPasswordHasher<User> can be resolved
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// Configure Cookie Authentication
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
