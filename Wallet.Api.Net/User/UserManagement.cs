using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Api.Net.User
{
    public static class UserManagement
    {
        public static async Task<bool> CheckIfUserExists(string user, string authToken)
        {
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.budgetbakers.com/api/v1/") })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("X-Token", authToken);

                var result = await client.GetAsync($"user/exists/{user}");

                return result.StatusCode == HttpStatusCode.OK;
            }
        }
    }
}
