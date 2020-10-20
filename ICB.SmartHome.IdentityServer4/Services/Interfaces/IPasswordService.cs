using ICB.SmartHome.IdentityServer4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICB.SmartHome.IdentityServer4.Services.Interfaces
{
    public interface IPasswordService
    {
        public Task<string> GenerateNewSalt();

        public Task<string> HashPassword(string plainPassword, string salt);

        public Task<bool> CheckPasswordValidity(User user, string passwordToComp);


    }
}
