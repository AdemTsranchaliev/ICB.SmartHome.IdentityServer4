using ICB.SmartHome.IdentityServer4.Data;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ICB.SmartHome.IdentityServer4.Services
{
    public class ProfileService : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var httpAccesor = new HttpContextAccessor();
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;database=SmartHomeIdentity;trusted_connection=yes;");
            var contetxDb = new AppDbContext(optionsBuilder.Options);


            var subjectId = context.Subject.GetSubjectId();
            var user = contetxDb.users.FirstOrDefault(x=>x.Id.ToString()==subjectId);


            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Email,user.Email),
            };

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
        }
    }
}
