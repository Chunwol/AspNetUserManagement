﻿using Lunimedia.Models;
using Lunimedia.Repositorys;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace Lunimedia.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository rep = new UserRepository();
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
            rep.AddUser(model.Id, model.Username, hashedPassword, salt);

            return true;
        }

        public bool VerifyId(string userId)
        {
            UserModel user = rep.GetUserByUserId(userId);
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
            UserModel user = rep.GetUserByUserId(userId);
            return user;
        }

        public UserModel GetUserByUserPk(Guid userPk)
        {
            UserModel user = rep.GetUserByUserPk(userPk);
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