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
    /// Functions for creating <see cref="WalletCategory"/>s on the server
    /// </summary>
    public static class CategoryCreation
    {
        /// <summary>
        /// Creates a new Category for the specified user, given a valid API token
        /// <para/>Only the name of the new category is a required property.
        /// <para/>Default values for other properties:<para/>
        ///     color:              #13BA78,
        ///     icon:               0,
        ///     defaultType:        <see cref="WalletCategoryType"/>.Expense,
        ///     position:           last + 1000,
        /// </summary>
        /// <param name="authUser">The e-mail address of the user to whom the category should be added</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user)</param>
        /// <param name="categoryToCreate">Account object that specifies the properties of the category to be created</param>
        /// <returns>Returns the unique Id of the newly created category</returns>
        public static async Task<string> CreateNew(string authUser, string authToken, WalletCategory categoryToCreate)
        {
            var postConformObject = new
            {
                name = categoryToCreate.Name,
                color = categoryToCreate.Color,
                icon = categoryToCreate.Icon,
                defaultType = categoryToCreate.DefaultType,
                position = categoryToCreate.Position,
            };

            var urlSpecifier = "category";
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
