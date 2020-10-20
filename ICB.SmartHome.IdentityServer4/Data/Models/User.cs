using ICB.SmartHome.IdentityServer4.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICB.SmartHome.IdentityServer4.Data
{
    public class User
    {
        public User()
        {

        }
        public User(string email,string password,string firstName,string lastName,string salt)
        {
            this.Email = email;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Salt = salt;
        }

        public User(RegisterViewModel model)
        {
            this.Email = model.Email;
            this.Password = model.Password;
            this.FirstName = model.FirstName;
            this.LastName = model.SecondName;
            this.Salt = model.Salt;
        }

        public int Id { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Salt { get; set; }
    }
}
