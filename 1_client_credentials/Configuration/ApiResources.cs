using System.Collections.Generic;
using IdentityServer4.Models;

namespace _1_client_credentials.Configuration
{
    public class ApiResources
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            var ar = new ApiResource("api1", "My API")
            {
                Scopes = new List<Scope>()
                {
                    new Scope("api1"),
                    new Scope("read"),
                    new Scope("readEnhanced") 
                }};

            return new List<ApiResource>(){ar};
        }
    }
}

