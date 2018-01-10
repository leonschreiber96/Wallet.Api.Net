using Newtonsoft.Json;

namespace Wallet.Api.Net.Model
{
    public class WalletAccount
    {
        /// <summary>
        /// Account Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of bank account
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Color of bank account in app
        /// </summary>
        [JsonProperty("color")]
        public string Color { get; set; }

        /// <summary>
        /// Whether to include bank account into dashboard statistics
        /// </summary>
        [JsonProperty("excludeFromStats")]
        public bool? ExcludeFromStats { get; set; }

        /// <summary>
        /// Whether to track location of records creation in this account
        /// </summary>
        [JsonProperty("gps")]
        public bool? Gps { get; set; }

        /// <summary>
        /// Initial amount of bank account
        /// </summary>
        [JsonProperty("initAmount")]
        public double? InitAmount { get; set; }

        /// <summary>
        /// Used for ordering of bank account
        /// </summary>
        [JsonProperty("position")]
        public int? Position { get; set; }
    }
}
