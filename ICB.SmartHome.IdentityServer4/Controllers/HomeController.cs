using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ICB.SmartHome.IdentityServer4.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ICB.SmartHome.IdentityServer4.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;database=SmartHomeIdentity;trusted_connection=yes;");
            var contetx = new AppDbContext(optionsBuilder.Options);
            var user = new User();
            user.Email = "test";
            user.FirstName = "test";
            user.LastName = "last";
            user.Password = "test;";


            contetx.Add(user);
            
            

            return View();
        }
    }
}
