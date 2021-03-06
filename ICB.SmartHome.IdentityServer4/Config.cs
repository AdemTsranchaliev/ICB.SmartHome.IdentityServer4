﻿using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using static IdentityModel.OidcConstants;
using GrantTypes = IdentityServer4.Models.GrantTypes;

namespace ICB.SmartHome.IdentityServer4
{
    public static class Config
    {

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),

                new IdentityResources.Email(),

            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {

                new ApiScope("api1", "My API")

            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                  new Client
                   {
                       ClientId = "mvc",
                       ClientName = "MVC Client",
                       AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                       ClientSecrets =
                       {
                           new Secret("secret".Sha256())
                       },

                       // where to redirect to after login
                       RedirectUris = { "http://localhost:5002/signin-oidc" },
                  
                       // where to redirect to after logout
                       PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                  
                       AllowedScopes = new List<string>
                       {
                           IdentityServerConstants.StandardScopes.OpenId,
                           IdentityServerConstants.StandardScopes.Profile,
                           "api1"
                       },

                       AllowOfflineAccess = true,
                       AlwaysIncludeUserClaimsInIdToken = true

                   }

            };
    }
}
