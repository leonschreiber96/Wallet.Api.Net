using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wallet.Api.Net.Model;

namespace Wallet.Api.Net.Currency
{
    /// <summary>
    /// Functions for creating <see cref="WalletCurrency"/>s on the server
    /// </summary>
    public static class CurrencyCreation
    {
        /// <summary>
        /// Creates a new Currency for the specified user, given a valid API token
        /// <para/>Only the code of the new currency and its ratio to the referential currency are required properties.
        /// <para/>Default values for other properties:<para/>
        ///     referential:        false,
        ///     position:           last + 1000,
        /// </summary>
        /// <param name="authUser">The e-mail address of the user to whom the account should be added</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user</param>
        /// <param name="currencyToCreate">Account object that specifies the properties of the account to be created</param>
        /// <returns>Returns the unique Id of the newly created account</returns>
        public static async Task<string> CreateNew(string authUser, string authToken, WalletCurrency currencyToCreate)
        {
            var postConformObject = new
            {
                code = currencyToCreate.Code,
                ratioToReferential = currencyToCreate.RatioToReferential,
                referential = currencyToCreate.Referential,
                position = currencyToCreate.Position
            };

            var urlSpecifier = "currency";
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

                return JsonConvert.DeserializeObject<dynamic>(responseJson).id;
            }
        }
    }
}
