using System.Collections.Generic;
using IdentityServer4.Models;

namespace _5_UI_server.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "codeClient",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.Code,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" },
                    RequireConsent = true,
                    AllowRememberConsent = false,
                    RedirectUris = new List<string> { "http://localhost:3000/callback" },
                    PostLogoutRedirectUris = new List<string> {"http://localhost:3000/logout"},
                },
                new Client
                {
                    ClientId = "codeClientRefresh",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.Code,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" },
                    RequireConsent = true,
                    AllowRememberConsent = false,
                    AllowOfflineAccess = true,
                    RedirectUris = new List<string> { "http://localhost:3000/callback" },
                    PostLogoutRedirectUris = new List<string> {"http://localhost:3000/logout"}
                }
            };
        }
    }
}
