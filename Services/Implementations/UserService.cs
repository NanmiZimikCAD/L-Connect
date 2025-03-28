using System.Threading.Tasks;
using L_Connect.Data;
using L_Connect.Models.Domain;
using L_Connect.Models.ViewModels.Client;
using L_Connect.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L_Connect.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DoesUserExistByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;
                
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;
                
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<bool> UpdateUserAsync(int id, ProfileViewModel model)
        {
            var user = await GetUserByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            // Update user properties from the ProfileViewModel
            user.FullName = model.FullName;
            user.Email = model.Email;
            user.ContactNumber = model.ContactNumber;

            // Save changes directly via the context
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
