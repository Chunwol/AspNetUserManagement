using System;


namespace AspNetUserManagement.Models
{
    public class UserModel
    {
        public Guid Pk { get; set; }
        public string Id { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Name { get; set; }

    }
}