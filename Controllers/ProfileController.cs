using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using L_Connect.Models.ViewModels.Client;
using L_Connect.Services.Interfaces;

namespace L_Connect.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: /Profile/
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                // User is not authenticated or claim missing, redirect to login.
                return RedirectToAction("Login", "Account");
            }
            
            int userId = int.Parse(userIdClaim.Value);
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ProfileViewModel
            {
                FullName = user.FullName,
                Email = user.Email,
                ContactNumber = user.ContactNumber
            };

            return View(model);
        }

        // POST: /Profile/
        [HttpPost]
        public async Task<IActionResult> Index(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                int userId = int.Parse(userIdClaim.Value);
                var result = await _userService.UpdateUserAsync(userId, model);
                if (result)
                {
                    // Redirect to the Dashboard action in ClientController after a successful update.
                    return RedirectToAction("Dashboard", "Client");
                }
                ModelState.AddModelError("", "Profile update failed.");
            }
            return View(model);
        }
    }
}
