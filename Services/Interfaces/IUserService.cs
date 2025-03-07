// Services/Interfaces/IUserService.cs
using System;
using System.Threading.Tasks;
using L_Connect.Models.Domain;

namespace L_Connect.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> DoesUserExistByEmail(string email);
        Task<User> GetUserByEmail(string email);
    }
}