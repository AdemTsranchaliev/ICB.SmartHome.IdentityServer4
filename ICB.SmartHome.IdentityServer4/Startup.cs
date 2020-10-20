using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ICB.SmartHome.IdentityServer4.Data;
using ICB.SmartHome.IdentityServer4.Services;
using ICB.SmartHome.IdentityServer4.Services.Interfaces;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Test;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace ICB.SmartHome.IdentityServer4
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).Assembly.GetName().Name;
            // uncomment, if you want to add an MVC-based UI
            services.AddControllersWithViews();

            services.AddDbContext<AppDbContext>(config =>
            {
                config.UseSqlServer(DataBaseConfig.DevelopmentConnection);
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters.ValidateAudience = false;
                        // base-address of your identityserver
                        options.Authority = "https://localhost:5001/";
                  
                    });
            services.AddHttpContextAccessor(); 
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUserService, UserService>()
                    .AddScoped<IPasswordService,PasswordService>();


            var builder = services.AddIdentityServer()
                                  .AddInMemoryIdentityResources(Config.IdentityResources)
                                  .AddInMemoryApiScopes(Config.ApiScopes)
                                  .AddInMemoryClients(Config.Clients)
                                  .AddInMemoryIdentityResources(
                                           new List<IdentityResource>

                                             {
                                                new IdentityResources.OpenId(),
                                                new IdentityResources.Profile(),
                                                new IdentityResources.Email()
                                             });
                                   

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>()
             .AddTransient<IProfileService, ProfileService>();

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
