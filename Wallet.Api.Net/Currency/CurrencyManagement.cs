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
    /// Functions for retrieving, updating and deleting of currencies
    /// </summary>
    public static class CurrencyManagement
    {
        /// <summary>
        /// Returns the currency with the specified id if it belongs to the specified user
        /// </summary>
        /// <param name="authUser">The e-mail address of the user whose currency should be retrieved</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user)</param>
        /// <param name="currencyId">The unique id of the currency that should be retrieved</param>
        public static async Task<WalletCurrency> GetByIdAsync(string authUser, string authToken, string currencyId)
        {
            var urlSpecifier = $"currency/{currencyId}";

            using (var client = new HttpClient { BaseAddress = new Uri(Constants.BaseUrl) })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(Constants.TokenHeaderKey, authToken);
                client.DefaultRequestHeaders.Add(Constants.UserHeaderKey, authUser);

                var response = await client.GetAsync(urlSpecifier);
                var responseJson = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<WalletCurrency>(responseJson);

                return result;
            }
        }

        /// <summary>
        /// Updates the specified currency. Properties that are null won't be changed.
        /// </summary>
        /// <param name="authUser">The e-mail address of the user to whom the currency should be added</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user)</param>
        /// <param name="currencyToUpdate">Currency object that specifie the properties of the currency to be updated</param>
        /// <returns>Returns the unique Id of the newly created currency</returns>
        public static async Task<string> UpdateAsync(string authUser, string authToken, WalletCurrency currencyToUpdate)
        {
            if (currencyToUpdate.Code == null)
                throw new ArgumentException("Code of updated currency object can't be null");

            if (currencyToUpdate.Id == null)
                throw new ArgumentException("Id of updated currency object can't be null");

            var putConformObject = new
            {
                code = currencyToUpdate.Code,
                ratioToReferential = currencyToUpdate.RatioToReferential,
                referential = currencyToUpdate.Referential,
                position = currencyToUpdate.Position
            };

            var urlSpecifier = $"currency/{currencyToUpdate.Id}";
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

                return JsonConvert.DeserializeObject<dynamic>(responseJson).id.ToString();
            }
        }

        /// <summary>
        /// Permanently deletes a user's currency with the specified id
        /// </summary>
        /// <param name="authUser">The e-mail address of the user to whom the currency should be added</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user)</param>
        /// <param name="currencyId">The id of the currency to be deleted</param>
        public static async void DeleteAsync(string authUser, string authToken, string currencyId)
        {
            if (currencyId == null)
                throw new ArgumentException("Id of deleted currency can't be null");

            var urlSpecifier = $"currency/{currencyId}";

            using (var client = new HttpClient { BaseAddress = new Uri(Constants.BaseUrl) })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(Constants.TokenHeaderKey, authToken);
                client.DefaultRequestHeaders.Add(Constants.UserHeaderKey, authUser);

                await client.DeleteAsync(urlSpecifier);
            }
        }
    }
}
