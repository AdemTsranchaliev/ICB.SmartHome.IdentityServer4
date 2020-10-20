using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ICB.SmartHome.IdentityServer4.Data;
using ICB.SmartHome.IdentityServer4.Data.ViewModels;
using ICB.SmartHome.IdentityServer4.Services;
using ICB.SmartHome.IdentityServer4.Services.Interfaces;
using IdentityModel;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace ICB.SmartHome.IdentityServer4.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserService _userService;


        public AuthController(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            this.httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register([FromBody]RegisterViewModel model)
        {

            var temp = _userService.RegisterUser(model);

            if (temp != null)
            {
                return new OkResult();

            }

            return new BadRequestResult();

        }   

        [HttpPost]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordViewModel model)
        {

            var accessToken = Request.Headers[HeaderNames.Authorization];
            var email = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type ==ClaimTypes.Email).Value;

            model.Email = email;

            var result = _userService.ChangePassword(model).Result;

            if (result)
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();

            }


        }

    }
}
