using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stash.Services.RequestModels.AccountKey
{
    public class AccountKeyResponse
    {
        public string Email { get; set; }
        public string AccountKey { get; set; }
    }
}
