using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wallet.Api.Net.Model;

namespace Wallet.Api.Net.Category
{
    /// <summary>
    /// Functions for retrieving <see cref="WalletCategory"/>s from the servers
    /// </summary>
    public static class CategoryCollection
    {
        /// <summary>
        /// Returns all Categories for a specified user, given a valid API token 
        /// </summary>
        /// <param name="authUser">The e-mail address of the user whose categories should be retrieved</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user</param>
        public static async Task<IEnumerable<WalletCategory>> GetAll(string authUser, string authToken)
        {
            var urlSpecifier = "categories";

            using (var client = new HttpClient { BaseAddress = new Uri(Constants.BaseUrl) })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(Constants.TokenHeaderKey, authToken);
                client.DefaultRequestHeaders.Add(Constants.UserHeaderKey, authUser);

                var response = await client.GetAsync(urlSpecifier);
                var responseJson = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<WalletCategory>>(responseJson);

                return result;
            }
        }
    }
}
