using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wallet.Api.Net.Model;

namespace Wallet.Api.Net.Record
{
    /// <summary>
    /// Functions for retrieving, updating and deleting of records
    /// </summary>
    public static class RecordManagement
    {
        /// <summary>
        /// Returns the record with the specified id if it belongs to the specified user
        /// </summary>
        /// <param name="authUser">The e-mail address of the user whose record should be retrieved</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user</param>
        /// <param name="recordId">The unique id of the record that should be retrieved</param>
        public static async Task<WalletRecord> GetById(string authUser, string authToken, string recordId)
        {
            var urlSpecifier = $"record/{recordId}";

            using (var client = new HttpClient { BaseAddress = new Uri(Constants.BaseUrl) })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(Constants.TokenHeaderKey, authToken);
                client.DefaultRequestHeaders.Add(Constants.UserHeaderKey, authUser);

                var response = await client.GetAsync(urlSpecifier);
                var responseJson = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<WalletRecord>(responseJson);

                return result;
            }
        }

        /// <summary>
        /// Updates the specified record. Properties that are null won't be changed.
        /// </summary>
        /// <param name="authUser">The e-mail address of the user to whom the record should be added</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user)</param>
        /// <param name="recordToUpdate">Record object that specifie the properties of the record to be updated</param>
        /// <returns>Returns the unique Id of the newly created record</returns>
        public static async Task<string> Update(string authUser, string authToken, WalletRecord recordToUpdate)
        {
            if (recordToUpdate.Id == null)
                throw new ArgumentException("Id of updated record object can't be null");

            var putConformObject = new
            {
                currencyId = recordToUpdate.CurrencyId,
                accountId = recordToUpdate.AccountId,
                categoryId = recordToUpdate.CategoryId,
                amount = recordToUpdate.Amount,
                paymentType = recordToUpdate.PaymentType,
                note = recordToUpdate.Note,
                date = recordToUpdate.Date.ToString(),
                recordState = recordToUpdate.RecordState
            };

            var urlSpecifier = $"record/{recordToUpdate.Id}";
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
        /// Permanently deletes a user's record with the specified id
        /// </summary>
        /// <param name="authUser">The e-mail address of the user to whom the record should be added</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user)</param>
        /// <param name="recordId">The id of the record to be deleted</param>
        public static async void Delete(string authUser, string authToken, string recordId)
        {
            if (recordId == null)
                throw new ArgumentException("Id of deleted record object can't be null");

            var urlSpecifier = $"record/{recordId}";

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
