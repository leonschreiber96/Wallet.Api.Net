using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wallet.Api.Net.Model;

namespace Wallet.Api.Net.Account
{
    /// <summary>
    /// Functions for creating bank accounts on the server
    /// </summary>
    public static class AccountCreation
    {
        /// <summary>
        /// Creates a new account for the specified user, given a valid API token
        /// <para/>Only the name of the new account is a required property.
        /// <para/>Default values for other properties:<para/>
        ///     color:              #13BA78,
        ///     excludeFromStats:   false,
        ///     gps:                true,
        ///     initAmount:         0,
        ///     position:           last + 1000,
        /// </summary>
        /// <param name="authUser">The e-mail address of the user to whom the account should be added</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user)</param>
        /// <param name="accountToCreate">Account object that specifies the properties of the account to be created</param>
        /// <returns>Returns the unique Id of the newly created account</returns>
        public static async Task<string> CreateNew(string authUser, string authToken, WalletAccount accountToCreate)
        {
            if (accountToCreate.Name == null)
                throw new ArgumentException("Account name can't be null");

            var postConformObject = new
            {
                id = accountToCreate.Id,
                name = accountToCreate.Name,
                color = accountToCreate.Color,
                excludeFromStats = accountToCreate.ExcludeFromStats,
                gps = accountToCreate.Gps,
                initAmount = accountToCreate.InitAmount,
                position = accountToCreate.Position
            };

            var urlSpecifier = "account";
            var contentType = "application/json";
            var contentJson = JsonConvert.SerializeObject(postConformObject);
            var content = new StringContent(contentJson, Encoding.Default, contentType);

            using (var client = new HttpClient { BaseAddress = new Uri(Constants.BaseUrl) })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(Constants.TokenHeaderKey, authToken);
                client.DefaultRequestHeaders.Add(Constants.UserHeaderKey, authUser);

                var response = await client.PostAsync(urlSpecifier, content);
                var responseJson = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<dynamic>(responseJson).id.ToString().ToString();
            }
        }
    }
}
