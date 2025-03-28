using L_Connect.Models.Domain;
using L_Connect.Models.ViewModels.Client;
using System.Threading.Tasks;

namespace L_Connect.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> DoesUserExistByEmail(string email);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(int id, ProfileViewModel model);
    }
}
