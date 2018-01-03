using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Newtonsoft.Json;

namespace Wallet.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private const string Token = "9b0279ac-8005-455a-99d5-9c6fe3eb0aec";

        [HttpGet]
        public async Task<string> Exists(string mail)
        {
            var retVal = await Wallet.Api.User.Functions.CheckIfUserExists(mail, Token);
            return $"Der Nutzer {mail} existiert {(retVal ? "" : "nicht")}";
        }
    }
}
