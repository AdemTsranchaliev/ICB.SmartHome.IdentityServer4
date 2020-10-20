using ICB.SmartHome.IdentityServer4.Data;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ICB.SmartHome.IdentityServer4.Services
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;database=SmartHomeIdentity;trusted_connection=yes;");
            var contetxDb = new AppDbContext(optionsBuilder.Options);
         
            var user = contetxDb.users.ToList().Where(x => x.Email == context.UserName).FirstOrDefault();
            if (user!=null)
            {
                var pass = BCrypt.Net.BCrypt.HashPassword(context.Password, user.Salt);

                if (pass.Equals(user.Password))
                {
                    context.Result = new GrantValidationResult(user.Id.ToString(), "password", null, "local", null);
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "The username and password do not match", null);
                }
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "The username and password do not match", null);

            }

        }

    }
}
