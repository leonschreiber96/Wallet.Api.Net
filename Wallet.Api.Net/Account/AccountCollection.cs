﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wallet.Api.Net.Model;

namespace Wallet.Api.Net.Account
{
    /// <summary>
    /// Functions for retrieving bank accounts from the Server
    /// </summary>
    public static class AccountCollection
    {
        /// <summary>
        /// Returns all Accounts for a specified user, given a valid API token 
        /// </summary>
        /// <param name="authUser">The e-mail address of the user whose accounts should be retrieved</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user)</param>
        public static async Task<IEnumerable<WalletAccount>> GetAll(string authUser, string authToken)
        {
            var urlSpecifier = "accounts";

            using (var client = new HttpClient { BaseAddress = new Uri(Constants.BaseUrl) })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(Constants.TokenHeaderKey, authToken);
                client.DefaultRequestHeaders.Add(Constants.UserHeaderKey, authUser);

                var response = await client.GetAsync(urlSpecifier);
                var responseJson = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<WalletAccount>>(responseJson);

                return result;
            }
        }
    }
}
