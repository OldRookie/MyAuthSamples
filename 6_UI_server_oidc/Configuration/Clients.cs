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
                }
            };
        }
    }
}