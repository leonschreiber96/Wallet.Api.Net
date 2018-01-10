using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Wallet.Api.Net.Model
{
    public class WalletRecord
    {
        /// <summary>
        /// The unique ID of the record
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The unique id of the record's currency
        /// </summary>
        [JsonProperty("currencyId")]
        public string CurrencyId { get; set; }

        /// <summary>
        /// The unique id of the record's account
        /// </summary>
        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        /// <summary>
        /// The unique id of the record's category
        /// </summary>
        [JsonProperty("categoryId")]
        public string CategoryId { get; set; }

        /// <summary>
        /// Record amount. Positive/Negative means income/expense
        /// </summary>
        [JsonProperty("amount")]
        public double Amount { get; set; }

        /// <summary>
        /// Type of Payment
        /// </summary>
        [JsonProperty("paymentType")]
        public WalletPaymentType PaymentType { get; set; }

        /// <summary>
        /// A note describing the record
        /// </summary>
        [JsonProperty("note")]
        public string Note { get; set; }

        /// <summary>
        /// Date of record creation. If none provided, current date in UTC will be added
        /// </summary>
        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// State of record. If not provided, the record will have WalletRecordState.Cleared
        /// </summary>
        [JsonProperty("recordState")]
        public WalletRecordState? RecordState { get; set; }
    }
}
