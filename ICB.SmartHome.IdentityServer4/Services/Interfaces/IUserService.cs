using ICB.SmartHome.IdentityServer4.Data;
using ICB.SmartHome.IdentityServer4.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICB.SmartHome.IdentityServer4.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User> RegisterUser(RegisterViewModel model);

        public Task<bool> ChangePassword(ChangePasswordViewModel model);

        public Task<User> GetUserByEmail(string email);

        public Task<User> GetUserById(int id);



    }
}
