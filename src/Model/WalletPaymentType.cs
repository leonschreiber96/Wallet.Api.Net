using System;
using System.Collections.Generic;
using System.Text;

namespace Wallet.Api.Net.Model
{
    public enum  WalletPaymentType
    {
        Cash,
        DebitCard,
        CreditCard,
        Transfer,
        Voucher,
        MobilePayment,
        WebPayment
    }
}
