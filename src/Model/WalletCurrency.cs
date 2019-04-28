using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Wallet.Api.Net.Model
{
    public class WalletCurrency
    {
        /// <summary>
        /// The unique ID of the currency
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Currency Code
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Conversion ratio to referential currency (if this is the referential, then 1)
        /// </summary>
        [JsonProperty("ratioToReferential")]
        public double RatioToReferential { get; set; }

        /// <summary>
        /// Flag that determines, whether this is the referential currency
        /// </summary>
        [JsonProperty("referential")]
        public bool? Referential { get; set; }

        /// <summary>
        /// Used for ordering of currency
        /// </summary>
        [JsonProperty("position")]
        public int? Position { get; set; }
    }
}
