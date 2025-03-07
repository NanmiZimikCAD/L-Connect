// Services/Implementations/UserService.cs
using System.Threading.Tasks;
using L_Connect.Data;
using L_Connect.Models.Domain;
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
    }
}