using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wallet.Api.Net.Balance
{
    /// <summary>
    /// Functions for retrieving the balance of specific bank accounts
    /// </summary>
    public static class AccountBalance
    {
        /// <summary>
        /// Returns the current overall balance for a specified account of the given user, given a valid API token 
        /// </summary>
        /// <param name="authUser">The e-mail address of the user whose balance should be retrieved</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user)</param>
        /// <param name="accountId">The unique Id of the account whose balance should be retrieved</param>
        public static async Task<double> GetAsync(string authUser, string authToken, string accountId)
        {
            var urlSpecifier = $"balance/{accountId}";

            using (var client = new HttpClient { BaseAddress = new Uri(Constants.BaseUrl) })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(Constants.TokenHeaderKey, authToken);
                client.DefaultRequestHeaders.Add(Constants.UserHeaderKey, authUser);

                var response = await client.GetAsync(urlSpecifier);
                var responseJson = await response.Content.ReadAsStringAsync();

                var result = (double)JsonConvert.DeserializeObject<dynamic>(responseJson).amount;

                return result;
            }
        }
    }
}
