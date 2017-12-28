using System.Collections.Generic;
using IdentityServer4.Models;

namespace _6_UI_server_oidc.Configuration
{
    public class IdentResources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("sergioScope", new List<string>(){"sergioClaim1","sergioClaim2"})
            };
        }
    }
}