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
    /// Functions for retrieving, updating and deleting of categories
    /// </summary>
    public static class CategoryManagement
    {
        /// <summary>
        /// Returns the category with the specified id if it belongs to the specified user
        /// </summary>
        /// <param name="authUser">The e-mail address of the user whose category should be retrieved</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user)</param>
        /// <param name="categoryId">The unique id of the category that should be retrieved</param>
        public static async Task<WalletCategory> GetById(string authUser, string authToken, string categoryId)
        {
            var urlSpecifier = $"category/{categoryId}";

            using (var client = new HttpClient { BaseAddress = new Uri(Constants.BaseUrl) })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add(Constants.TokenHeaderKey, authToken);
                client.DefaultRequestHeaders.Add(Constants.UserHeaderKey, authUser);

                var response = await client.GetAsync(urlSpecifier);
                var responseJson = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<WalletCategory>(responseJson);

                return result;
            }
        }

        /// <summary>
        /// Updates the specified category. Properties that are null won't be changed.
        /// </summary>
        /// <param name="authUser">The e-mail address of the user to whom the category should be added</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user)</param>
        /// <param name="categoryToUpdate">Category object that specifie the properties of the category to be updated</param>
        /// <returns>Returns the unique Id of the newly created category</returns>
        public static async Task<string> Update(string authUser, string authToken, WalletCategory categoryToUpdate)
        {
            if (categoryToUpdate.Id == null)
                throw new ArgumentException("Id of updated category object can't be null");

            var putConformObject = new
            {
                name = categoryToUpdate.Name,
                color = categoryToUpdate.Color,
                icon = categoryToUpdate.Icon,
                defaultType = categoryToUpdate.DefaultType.ToValidApiString(),
                position = categoryToUpdate.Position
            };

            var urlSpecifier = $"category/{categoryToUpdate.Id}";
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
        /// Permanently deletes a user's category with the specified id
        /// </summary>
        /// <param name="authUser">The e-mail address of the user to whom the category should be added</param>
        /// <param name="authToken">A valid API token (linked to the e-mail of the specified user)</param>
        /// <param name="categoryId">The id of the category to be deleted</param>
        public static async void Delete(string authUser, string authToken, string categoryId)
        {
            if (categoryId == null)
                throw new ArgumentException("Id of deleted category object can't be null");

            var urlSpecifier = $"category/{categoryId}";

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
