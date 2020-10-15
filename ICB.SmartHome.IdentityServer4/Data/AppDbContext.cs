using ICB.SmartHome.IdentityServer4.Data;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICB.SmartHome.IdentityServer4.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
              : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)

        {

            if (!builder.IsConfigured)

                builder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;database=SmartHomeIdentity;trusted_connection=yes;");

        }

        public DbSet<Role> roles { get; set; }
        public DbSet<User> users { get; set; }

    }
}
