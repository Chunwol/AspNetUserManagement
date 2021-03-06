using AspNetUserManagement.Models;
using System;

namespace AspNetUserManagement.Services
{
    public interface IUserService
    {
        UserModel GetUserByUserId(string userId);
        UserModel GetUserByUserPk(Guid userPk);
        bool Register(RegisterModel model);
        bool ValidatePassword(UserModel user, LoginModel model);
        bool VerifyId(string userId);
    }
}