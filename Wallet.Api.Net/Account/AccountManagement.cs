using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wallet.Api.Net.Model;

namespace Wallet.Api.Net.Account
{
    public static class AccountManagement
    {
        /// <summary>
        /// Returns the account with the specified id if it belongs to the specified user
        /// </summary>
        /// <param name="authUser">The e-mail address of the user whose account should be retrieved</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user</param>
        /// <param name="accountId">The unique id of the account that should be retrieved</param>
        public static async Task<WalletAccount> GetById(string authUser, string authToken, string accountId)
        {
            var urlSpecifier = $"account/{accountId}";

            using (var client = new HttpClient { BaseAddress = new Uri(Constants.BaseUrl) })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(Constants.TokenHeaderKey, authToken);
                client.DefaultRequestHeaders.Add(Constants.UserHeaderKey, authUser);

                var response = await client.GetAsync(urlSpecifier);
                var responseJson = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<WalletAccount>(responseJson);

                return result;
            }
        }

        /// <summary>
        /// Updates the specified account. Properties that are null won't be changed.
        /// </summary>
        /// <param name="authUser">The e-mail address of the user to whom the account should be added</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user</param>
        /// <param name="accountToUpdate">Account object that specifie the properties of the account to be updated</param>
        /// <returns>Returns the unique Id of the newly created account</returns>
        public static async Task<string> Update(string authUser, string authToken, WalletAccount accountToUpdate)
        {
            if (accountToUpdate.Id == null)
                throw new ArgumentException("Id of updated account object can't be null");

            var putConformObject = new
            {
                name = accountToUpdate.Name,
                color = accountToUpdate.Color,
                excludeFromStats = accountToUpdate.ExcludeFromStats,
                gps = accountToUpdate.Gps,
                initAmount = accountToUpdate.InitAmount,
                position = accountToUpdate.Position
            };

            var urlSpecifier = $"account/{accountToUpdate.Id}";
            var contentType = "application/json";
            var contentJson = JsonConvert.SerializeObject(putConformObject);
            var content = new StringContent(contentJson, Encoding.Default, contentType);

            using (var client = new HttpClient { BaseAddress = new Uri(Constants.BaseUrl) })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(Constants.TokenHeaderKey, authToken);
                client.DefaultRequestHeaders.Add(Constants.UserHeaderKey, authUser);

                var response = await client.PutAsync(urlSpecifier, content);
                var responseJson = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<dynamic>(responseJson).id;
            }
        }

        /// <summary>
        /// Permanently deletes a user's account with the specified id
        /// </summary>
        /// <param name="authUser">The e-mail address of the user to whom the account should be added</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user</param>
        /// <param name="accountId">The id of the account to be deleted</param>
        public static async void Delete(string authUser, string authToken, string accountId)
        {
            if (accountId == null)
                throw new ArgumentException("Id can't be null");

            var urlSpecifier = $"account/{accountId}";

            using (var client = new HttpClient { BaseAddress = new Uri(Constants.BaseUrl) })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(Constants.TokenHeaderKey, authToken);
                client.DefaultRequestHeaders.Add(Constants.UserHeaderKey, authUser);

                var response = await client.DeleteAsync(urlSpecifier);
            }
        }
    }
}
