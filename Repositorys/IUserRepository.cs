using AspNetUserManagement.Models;
using System;

namespace AspNetUserManagement.Services
{
    public interface IUserRepository
    {
        void AddUser(string userID, string name, string password, byte[] passwordSalt);
        UserModel GetUserByUserId(string userId);
        UserModel GetUserByUserPk(Guid userPk);
    }
}