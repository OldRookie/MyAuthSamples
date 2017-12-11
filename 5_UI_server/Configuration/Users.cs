using System.Collections.Generic;
using IdentityServer4.Test;

namespace _5_UI_server.Configuration
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
                    Password = "password"
                }
            };
        }
}
}
