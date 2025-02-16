using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using L_Connect.Models;
using L_Connect.Models.ViewModels.Auth;
using L_Connect.Data;
using L_Connect.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;  // Added this for IPasswordHasher
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

//AccountController is responsible for handling all user authentication and account management functionalities

namespace L_Connect.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountController(
        ILogger<AccountController> logger,
        ApplicationDbContext context,
        IPasswordHasher<User> passwordHasher)
        {
            _logger = logger;
            _context = context;
            _passwordHasher = passwordHasher;
        }
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                //check if user exists
                var user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == model.Email);

                //verify password
                if (user != null)
                {
                    var result = _passwordHasher.VerifyHashedPassword(
                        user,
                        user.PasswordHash,
                        model.Password
                    );

                    if (result == PasswordVerificationResult.Success)
                    {
                        _logger.LogInformation($"User found: {user.Email}, Role: {user.Role.RoleName}");

                        // Redirect based on role
                        if (user.Role.RoleName.ToUpper() == "ADMIN")
                        {
                            return RedirectToAction("Dashboard", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Dashboard", "Client");
                        }
                    }
                }

                _logger.LogWarning($"Login failed for email: {model.Email}");
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Database error during login: {ex.Message}");
                ModelState.AddModelError("", "An error occurred during login");
                return View(model);
            }
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Check if email already exists
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Email already registered");
                    return View(model);
                }

                // Get the CLIENT role
                var clientRole = await _context.Roles
                    .FirstOrDefaultAsync(r => r.RoleName == "CLIENT");

                if (clientRole == null)
                {
                    ModelState.AddModelError("", "Registration error: Role not found");
                    return View(model);
                }

                // Create new user
                var user = new User
                {
                    Email = model.Email,
                    FullName = model.FullName,
                    ContactNumber = model.ContactNumber,
                    RoleId = clientRole.RoleId,
                    CreatedAt = DateTime.UtcNow
                };

                // Hash password
                user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

                // Save user
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Redirect to login with success message
                TempData["Message"] = "Registration successful! Please login.";
                return RedirectToAction(nameof(Login));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Registration error: {ex.Message}");
                ModelState.AddModelError("", "An error occurred during registration");
                return View(model);
            }
        }
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to home page
            return RedirectToAction("Index", "Home");
        }
    }
}