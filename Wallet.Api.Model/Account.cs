using System;

namespace Wallet.Api.Model
{
    public class Account
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public bool ExcludeFromStats { get; set; }

        public bool Gps { get; set; }

        public double InitAmount { get; set; }

        public int Position { get; set; }
    }
}
