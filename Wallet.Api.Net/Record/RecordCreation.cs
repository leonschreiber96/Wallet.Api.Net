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
    /// Functions for creating <see cref="WalletRecord"/>s on the server
    /// </summary>
    public static class RecordCreation
    {
        /// <summary>
        /// Creates a new Record for the specified user, given a valid API token
        /// <para/>Required Properties;<para/>
        ///     currencyId,
        ///     accountId,
        ///     categoryId,
        ///     amount,
        ///     paymentType
        /// <para/>Default values for other properties:<para/>
        ///     note:              "",
        ///     date:              DateTime.UtcNow,
        ///     recordState:       <see cref="WalletRecordState"/>.Cleared,
        /// </summary>
        /// <param name="authUser">The e-mail address of the user to whom the record should be added</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user)</param>
        /// <param name="recordToCreate">Account object that specifies the properties of the record to be created</param>
        /// <returns>Returns the unique Id of the newly created record</returns>
        public static async Task<string> CreateNew(string authUser, string authToken, WalletRecord recordToCreate)
        {
            var postConformObject = new
            {
                currencyId = recordToCreate.CurrencyId,
                accountId = recordToCreate.AccountId,
                categoryId = recordToCreate.CategoryId,
                amount = recordToCreate.Amount,
                paymentType = recordToCreate.PaymentType,
                note = recordToCreate.Note,
                date = recordToCreate.Date.ToString(),
                recordState = recordToCreate.RecordState
            };

            var urlSpecifier = "record";
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

                return JsonConvert.DeserializeObject<dynamic>(responseJson).id.ToString();
            }
        }
    }
}
