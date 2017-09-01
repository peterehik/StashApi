using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stash.Services.RequestModels.AccountKey
{
    public class AccountKeyRequest : Request
    {
        public string Email { get; set; }
        public string Key { get; set; }
    }
}
