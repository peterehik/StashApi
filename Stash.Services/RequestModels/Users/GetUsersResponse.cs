using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stash.Services.RequestModels.Users
{
    public class GetUsersResponse
    {
        public List<UserResponseItem> Users { get; set; } 
    }

    public class UserResponseItem
    {
        public string email { get; set; }
        public string phone_number { get; set; }
        public string metadata { get; set; }
        public string password { get; set; }
        public string key { get; set; }
        public string account_key { get; set; }
        public string full_name { get; set; }
    }
}
