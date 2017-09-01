using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stash.DataSource.Entities;

namespace Stash.DataSource.Logic
{
    public interface IUsersLogic
    {
       Task AddUser(Users user);

       Task<List<Users>> GetAll();

       Task<List<Users>> GetUsersByQuery(string query);

        void UpdateUserAccountKey(int userId, string accountKey);

    }
}
