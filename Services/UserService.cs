using AspNetUserManagement.Models;
using AspNetUserManagement.Repositorys;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace AspNetUserManagement.Services
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository = new UserRepository();
        private byte[] CreateSalt()
        {
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            byte[] salt = new byte[32];
            random.GetNonZeroBytes(salt);

            return salt;
        }

        private string CreatePasswordHash(string pwd, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
              password: pwd,
              salt: salt,
              prf: KeyDerivationPrf.HMACSHA512,
              iterationCount: 10000,
              numBytesRequested: 256 / 8));

            return hashed;
        }


        public bool Register(RegisterModel model)
        {
            byte[] salt = CreateSalt();
            string hashedPassword = CreatePasswordHash(model.Password, salt);
            _userRepository.AddUser(model.Id, model.Username, hashedPassword, salt);

            return true;
        }

        public bool VerifyId(string userId)
        {
            UserModel user = _userRepository.GetUserByUserId(userId);
            if (user.Id != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public UserModel GetUserByUserId(string userId)
        {
            UserModel user = _userRepository.GetUserByUserId(userId);
            return user;
        }

        public UserModel GetUserByUserPk(Guid userPk)
        {
            UserModel user = _userRepository.GetUserByUserPk(userPk);
            return user;
        }

        public bool ValidatePassword(UserModel user, LoginModel model)
        {
            byte[] salt = Convert.FromBase64String(user.PasswordSalt);
            string hashedPassword = CreatePasswordHash(model.Password, salt);
            if (hashedPassword.Equals(user.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}