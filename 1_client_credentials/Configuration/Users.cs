using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Test;

namespace _1_client_credentials.Configuration
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
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "tiberio",
                    Password = "password",
                    Claims = new List<Claim>()
                    {
                        new Claim("myCustomEmail","tiberio@hotmail@com"),
                        new Claim("myCustomMobile","666998877"),
                    }
                }
            };
        }
    }
}
