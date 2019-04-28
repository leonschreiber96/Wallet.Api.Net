using System;
using System.Collections.Generic;
using System.Text;

namespace Wallet.Api.Net.Model
{
    public enum WalletRecordState
    {
        Reconciled,
        Cleared,
        Uncleared,
        Void
    }
}
