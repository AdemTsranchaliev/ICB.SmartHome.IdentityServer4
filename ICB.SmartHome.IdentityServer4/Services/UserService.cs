using ICB.SmartHome.IdentityServer4.Data;
using ICB.SmartHome.IdentityServer4.Data.ViewModels;
using ICB.SmartHome.IdentityServer4.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICB.SmartHome.IdentityServer4.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly IPasswordService _passwordService;


        public UserService(AppDbContext db,IPasswordService passwordService)
        {
            _db = db;
            _passwordService = passwordService;
        }

        public async Task<bool> ChangePassword(ChangePasswordViewModel model)
        {

            if (model.NewPassword==model.RepeatNewPassword)
            {
                User user = GetUserByEmail(model.Email).Result;

                if (_passwordService.CheckPasswordValidity(user,model.NewPassword).Result)
                {
                    var salt = _passwordService.GenerateNewSalt().Result;
                    var newHashedPassword = _passwordService.HashPassword(model.NewPassword,salt).Result;

                    user.Password = newHashedPassword;
                    user.Salt = salt;

                    _db.Update(user);

                    _db.SaveChanges();


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

        public async Task<User> GetUserByEmail(string email)
        {
            var user = _db.users.FirstOrDefault(x => x.Email == email);

            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = _db.users.FirstOrDefault(x => x.Id == id);

            return user;
        }

        public async Task<User> RegisterUser(RegisterViewModel model)
        {
            var isEmailExist = this.GetUserByEmail(model.Email);

            if (isEmailExist != null)
            {
                return null;
            }

            if (model.Password != model.RepeatPassword)
            {
                return null;
            }

            var salt = _passwordService.GenerateNewSalt().Result;
            var pass = _passwordService.HashPassword(model.Password,salt).Result;

            model.Password = pass;
            model.Salt = salt;

            var user = new User(model);

            _db.Add(user);
            _db.SaveChanges();

            return user;
        }
    }
}
