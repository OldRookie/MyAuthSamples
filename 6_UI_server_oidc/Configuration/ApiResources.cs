using System.Collections.Generic;
using IdentityServer4.Models;

namespace _6_UI_server_oidc.Configuration
{
    public class ApiResources
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api2", "My API")
            };
        }
    }
}
