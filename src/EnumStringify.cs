using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wallet.Api.Net.Model;

namespace Wallet.Api.Net
{
    internal static class EnumStringify
    {
        internal static string ToValidApiString(this WalletRecordState? walletRecordState)
        {
            var defaultString = walletRecordState.ToString();
            var jsonString = char.ToLowerInvariant(defaultString[0]) + defaultString.Substring(1);

            for (var i = 0; i < jsonString.Length; i++)
            {
                if (char.IsUpper(jsonString[i]))
                {
                    var @char = char.ToLowerInvariant(jsonString[i]);
                    jsonString = jsonString.Remove(i, 1);
                    jsonString = jsonString.Insert(i, $"_{@char}");
                }
            }

            return jsonString;
        }

        internal static string ToValidApiString(this WalletCategoryType? walletRecordState)
        {
            var defaultString = walletRecordState.ToString();
            var jsonString = char.ToLowerInvariant(defaultString[0]) + defaultString.Substring(1);

            for (var i = 0; i < jsonString.Length; i++)
            {
                if (char.IsUpper(jsonString[i]))
                {
                    var @char = char.ToLowerInvariant(jsonString[i]);
                    jsonString = jsonString.Remove(i, 1);
                    jsonString = jsonString.Insert(i, $"_{@char}");
                }
            }

            return jsonString;
        }

        internal static string ToValidApiString(this WalletPaymentType walletRecordState)
        {
            var defaultString = walletRecordState.ToString();
            var jsonString = char.ToLowerInvariant(defaultString[0]) + defaultString.Substring(1);

            for (var i = 0; i < jsonString.Length; i++)
            {
                if (char.IsUpper(jsonString[i]))
                {
                    var @char = char.ToLowerInvariant(jsonString[i]);
                    jsonString = jsonString.Remove(i, 1);
                    jsonString = jsonString.Insert(i, $"_{@char}");
                }
            }

            return jsonString;
        }
    }
}
