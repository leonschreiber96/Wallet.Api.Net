using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Wallet.Api.Model;

namespace Wallet.Api.Account
{
    public static class Functions
    {
        public static async Task<Model.Account> CheckIfUserExists(string email, string token)
        {
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.budgetbakers.com/api/v1/") })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("X-Token", token);
                client.DefaultRequestHeaders.Add("X-User", email);

                var result = await client.GetAsync("accounts");
            }
        }
    }
}
