using System;
using Wallet.Api.Net.Account;
using Wallet.Api.Net.Category;
using Wallet.Api.Net.Model;

namespace TestApp
{
    class Program
    {
        private const string Token = "9b0279ac-8005-455a-99d5-9c6fe3eb0aec";
        private const string Mail = "leonschreiber96@googlemail.com";

        static void Main(string[] args)
        {
            var test = CategoryCollection.GetAll(Mail, Token).Result;

            foreach (var testi in test)
            {
                Console.WriteLine(testi.Name);
            }

            Console.ReadLine();
        }

        static void GetAccountByIdTest()
        {
            var test = AccountManagement.GetById(Mail, Token, "15a5e55e-7bc6-404d-81e8-15ad200add9e").Result;

            Console.WriteLine(test.Id);
            Console.WriteLine(test.Name);
            Console.WriteLine(test.Color);
            Console.WriteLine(test.ExcludeFromStats);
            Console.WriteLine(test.Gps);
            Console.WriteLine(test.InitAmount);
            Console.WriteLine(test.Position);
        }

        static void CreateAccountTest()
        {
            var newAccount = new WalletAccount
            {
                Name =                          /*    Console.ReadLine(),                    */  "Test",
                Color =                         /*    Console.ReadLine(),                    */  "#13ba78",
                ExcludeFromStats =              /*    Convert.ToBoolean(Console.ReadLine()), */  true,
                Gps =                           /*    Convert.ToBoolean(Console.ReadLine()), */  false,
                InitAmount =                    /*    Convert.ToDouble(Console.ReadLine()),  */  400.1,
                Position =                      /*    Convert.ToInt32(Console.ReadLine())    */  1000
            };

            var x = AccountCreation.CreateNew(Mail, Token, newAccount).Result;

            Console.WriteLine(x);
        }
    }
}
