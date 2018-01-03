using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Wallet.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController
    {
        private const string Token = "9b0279ac-8005-455a-99d5-9c6fe3eb0aec";

        [HttpGet]
        public async Task<string> GetAll(string mail)
        {

        }
    }
}
