using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ICB.SmartHome.IdentityServer4.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ICB.SmartHome.IdentityServer4.Controllers
{
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult test(string email, string password,string firstName,string lastName)
        {

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            var stringSalt= Convert.ToBase64String(salt);

            var user = new User(email, savedPasswordHash, firstName,lastName,stringSalt);


            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;database=SmartHomeIdentity;trusted_connection=yes;");
            var contetx = new AppDbContext(optionsBuilder.Options);

            contetx.Add(user);
            contetx.SaveChanges();
            return new OkResult();
            
            


        }
    }
}
