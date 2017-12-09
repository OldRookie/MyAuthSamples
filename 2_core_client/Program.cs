using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace _2_core_client
{
    class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        static async Task MainAsync()
        {
            Console.WriteLine("Starting...");

            /////////////////////////////////////////////////
            // call api with a client credentials grant token
            /////////////////////////////////////////////////
            Console.WriteLine("call api with a client credentials grant token");
            var token = await GetClientCredentialsGrantToken();

            
            var client = new HttpClient();
            client.SetBearerToken(token);

            var response = await client.GetAsync("http://localhost:5001/api/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            //////////////////////////////////////////////////
            // call api with a password credentials grant token
            ///////////////////////////////////////////////////
            Console.WriteLine("\ncall api with a password credentials grant token");
            token = await GetPasswordCredentialsGrantToken();

            client = new HttpClient();
            client.SetBearerToken(token);

            response = await client.GetAsync("http://localhost:5001/api/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

        }

        public static async Task<string> GetClientCredentialsGrantToken()
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return null;
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return null;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");
            return tokenResponse.AccessToken;
        }

        public static async Task<string> GetPasswordCredentialsGrantToken()
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return null;
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "clientPwd", "pwdsecret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("tiberio","password","api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return null;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");
            return tokenResponse.AccessToken;
        }
    }
}
