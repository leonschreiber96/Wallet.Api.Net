using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wallet.Api.Net.Model;

namespace Wallet.Api.Net.Balance
{
    /// <summary>
    /// Functions for retrieving the overall balance of a user
    /// </summary>
    public static class OverallBalance
    {
        /// <summary>
        /// Returns the current overall balance for a specified user, given a valid API token 
        /// </summary>
        /// <param name="authUser">The e-mail address of the user whose balance should be retrieved</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user)</param>
        public static async Task<double> Get(string authUser, string authToken)
        {
            var urlSpecifier = "balance";

            using (var client = new HttpClient { BaseAddress = new Uri(Constants.BaseUrl) })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(Constants.TokenHeaderKey, authToken);
                client.DefaultRequestHeaders.Add(Constants.UserHeaderKey, authUser);

                var response = await client.GetAsync(urlSpecifier);
                var responseJson = await response.Content.ReadAsStringAsync();

                var result = (double) JsonConvert.DeserializeObject<dynamic>(responseJson).amount;

                return result;
            }
        }
    }
}
