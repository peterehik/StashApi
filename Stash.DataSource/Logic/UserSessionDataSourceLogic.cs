using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Stash.DataSource.Entities;

namespace Stash.DataSource.Logic
{
    public class UserSessionDataSourceLogic : IUsersLogic
    {
        private const string UserLock = "sessionUserLock";
        private readonly List<Users> _allUsers;
        private readonly Dictionary<string, List<Users>> _nameLookUp;
        private readonly Dictionary<string, List<Users>> _emailLookUp;
        private readonly Dictionary<string, List<Users>> _metaDataLookUp;


        private const string EmailLookUp = "EmailLookup";
        private const string NameLookUp = "NameLookup";
        private const string AllUsers = "AllUsers";
        private const string MetaDataLookUp = "MetaDataLookup";

        public UserSessionDataSourceLogic()
        {
            var session = HttpContext.Current.Session;

            if (session[AllUsers] == null || session[NameLookUp] == null  || session[EmailLookUp] == null)
            {
                session[AllUsers] = new List<Users>();
                session[NameLookUp] = new Dictionary<string, List<Users>>();
                session[EmailLookUp] = new Dictionary<string, List<Users>>();
                session[MetaDataLookUp] = new Dictionary<string, List<Users>>();

            }
            _allUsers = session[AllUsers] as List<Users>;
            _nameLookUp = session[NameLookUp] as Dictionary<string, List<Users>>;
            _emailLookUp = session[EmailLookUp] as Dictionary<string, List<Users>>;
            _metaDataLookUp = session[MetaDataLookUp] as Dictionary<string, List<Users>>;

        }

        private static void AddOrUpdateLookUp<T>(IDictionary<string, List<T>> lookUp, string key, T value)
        {
            key = key.ToUpper().Trim();
            if (lookUp.ContainsKey(key))
            {
                lookUp[key].Add(value);
            }
            else
            {
                lookUp.Add(key, new List<T>(){value});
            }
        }

        public async Task AddUser(Users user)
        {
            lock (UserLock)
            {
                user.Id = _allUsers.Count() + 1;
                _allUsers.Add(user);
                AddOrUpdateLookUp(_nameLookUp, user.FullName, user);
                AddOrUpdateLookUp(_emailLookUp, user.Email, user);
                foreach (var entity in user.MetaData.Split(new []{", "}, StringSplitOptions.RemoveEmptyEntries))
                {
                    AddOrUpdateLookUp(_metaDataLookUp, entity, user);
                }
            }
        }

        public void UpdateUserAccountKey(int userId, string accountKey)
        {
            var user = GetById(userId);
            user.AccountKey = accountKey;
        }

        private Users GetById(int userId)
        {
            return _allUsers[userId - 1];
        } 

        public async Task<List<Users>> GetAll()
        {
            lock (UserLock)
            {
                return Mapper.Map<List<Users>>(_allUsers);
            }
        }

        public async Task<List<Users>> GetUsersByQuery(string query)
        {
            if(string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("query");

            query = query.ToUpper().Trim();
            lock (UserLock)
            {
                var results = new List<Users>();
                if (_nameLookUp.ContainsKey(query))
                {
                    results.AddRange(_nameLookUp[query]);
                }

                if (_emailLookUp.ContainsKey(query))
                {
                    results.AddRange(_emailLookUp[query]);
                }

                if (_metaDataLookUp.ContainsKey(query))
                {
                    results.AddRange(_metaDataLookUp[query]);
                }
                

                return Mapper.Map<List<Users>>(results.Distinct(new UsersComparer()).ToList());
            }
           
        }

        public class UsersComparer : IEqualityComparer<Users>
        {
            public bool Equals(Users x, Users y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(Users obj)
            {
                return obj.Id.GetHashCode();
            }
        }
    }
}
