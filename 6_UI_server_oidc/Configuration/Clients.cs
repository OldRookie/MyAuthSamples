using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace _6_UI_server_oidc.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "iodcImplicitClient",
                    ClientName = "iodc Implicit Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    RedirectUris = { "http://localhost:5501/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5501/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "sergioScope"
                    }
                },
                new Client
                {
                    ClientId = "iodcHybridClient",
                    ClientName = "iodc Hybrid Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RedirectUris = { "http://localhost:5503/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5503/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "sergioScope",
                        "api2",
                    },
                    AllowOfflineAccess = true
                },
                new Client
                {
                    ClientId = "jsImplicitClient",
                    ClientName = "iodc Implicit Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "http://localhost:5504/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5504/index.html" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "sergioScope",
                        "api2",
                    },
                    AllowAccessTokensViaBrowser = true
                },
                new Client
                {
                    ClientId = "jsHybridClient",
                    ClientName = "iodc Hybrid Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    RedirectUris = { "http://localhost:5504/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5504/index.html" },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "sergioScope",
                        "api2",
                    },
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = true
                }
            };
        }
    }
}