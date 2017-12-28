using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Test;

namespace _6_UI_server_oidc.Configuration
{
    public class Users
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "efrain",
                    Password = "password",
                    Claims = new List<Claim>()
                    {
                        new Claim("name", "Efrain"),
                        new Claim("website", "https://efrain.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "tiberio",
                    Password = "password",
                    Claims = new List<Claim>()
                    {
                        new Claim("name", "Tiberiades"),
                        new Claim("website", "https://tiberiades.com"),
                        new Claim("sergioClaim1","sergioClaim1 value"),
                        new Claim("sergioClaim2","sergioClaim2 value"),
                    }
                }
            };
        }
    }
}
