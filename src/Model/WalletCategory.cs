using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Wallet.Api.Net.Model
{
    public class WalletCategory
    {
        /// <summary>
        /// The unique ID of the category
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of category
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Color of category in app
        /// </summary>
        [JsonProperty("color")]
        public string Color { get; set; }

        /// <summary>
        /// Category icon
        /// </summary>
        [JsonProperty("icon")]
        public WalletCategoryIcon? Icon { get; set; }

        /// <summary>
        /// Money flow type
        /// </summary>
        [JsonProperty("defaultType")]
        public WalletCategoryType? DefaultType { get; set; }

        /// <summary>
        /// Used for ordering of category
        /// </summary>
        [JsonProperty("position")]
        public int? Position { get; set; }
    }
}
