﻿using IdentityServer4.Models;
using System.Collections.Generic;

namespace MyFamilyManager.Identity.API
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
           new IdentityResource[]
           {
               new IdentityResources.OpenId(),
               new IdentityResources.Profile(),
               new IdentityResources.Email(),
           };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("api1", "My API")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "api1" }
                },

                // MVC client using code flow + pkce
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequirePkce = true,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    RedirectUris = { "http://localhost:5003/signin-oidc" },
                    FrontChannelLogoutUri = "http://localhost:5003/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:5003/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "api1" }
                },

                // SPA client using code flow + pkce
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Client",
                    ClientUri = "http://identityserver.io",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris =
                    {
                        "http://localhost:5002/index.html",
                        "http://localhost:5002/callback.html",
                        "http://localhost:5002/silent.html",
                        "http://localhost:5002/popup.html",
                    },

                    PostLogoutRedirectUris = { "http://localhost:5002/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5002" },

                    AllowedScopes = { "openid", "profile", "api1" }
                },
                new Client
                {
                    ClientName = "ReactApp",
                    ClientId = "reactclient",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:44303/callback"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:44303/"
                    },
                    AllowedScopes =
                    {
                        "openid", "profile", "api1"
                    },
                    ClientSecrets =
                    {
                        new Secret("reactclient".Sha256())
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:44303/"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}
