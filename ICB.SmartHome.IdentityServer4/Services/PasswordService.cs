using ICB.SmartHome.IdentityServer4.Data;
using ICB.SmartHome.IdentityServer4.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICB.SmartHome.IdentityServer4.Services
{
    public class PasswordService : IPasswordService
    {
        public PasswordService()
        {

        }

        public async Task<bool> CheckPasswordValidity(User user, string passwordToComp)
        {
            if (user != null)
            {
                var pass = BCrypt.Net.BCrypt.HashPassword(passwordToComp, user.Salt);

                if (pass.Equals(user.Password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<string> GenerateNewSalt()
        {
           return BCrypt.Net.BCrypt.GenerateSalt(13); 
        }

        public async Task<string> HashPassword(string plainPassword, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainPassword, salt);
        }
    }
}
