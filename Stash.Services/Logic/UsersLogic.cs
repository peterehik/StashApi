using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Stash.DataSource.Entities;
using Stash.Services.RequestModels.AccountKey;
using Stash.Services.RequestModels.Users;

namespace Stash.Services.Logic
{
    public class UsersLogic
    {
        private Random _random = new Random();

        private string[] _names = new string[] {"Peter", "Meltzer", "John", "Cena", "Roman", "Reigns", "Dave", "Bobby", "Roode"}; 
        private string[] _places = new string[]{"england", "london", "chelsea", "texas", "new york", "brooklyn", "queens", "albany", "dallas"};
        private string[] _emailProviders = {"gmail", "hotmail", "email", "poindexter", "stash", "wwe", "bankofamerica"};

        private DataSource.Logic.IUsersLogic _dataLogic;
        private AccountKeyLogic _accountKeyLogic = new AccountKeyLogic();

        public UsersLogic(DataSource.Logic.IUsersLogic dataLogic)
        {
            _dataLogic = dataLogic;
        }

        public async Task<GetUsersResponse> GetAllUsers()
        {
            var response = new GetUsersResponse();
            var users = await _dataLogic.GetAll();
            response.Users = Mapper.Map<List<UserResponseItem>>(users);
            return response;
        }

        public async Task<GetUsersResponse> GetUsersByQuery(string query)
        {
            var response = new GetUsersResponse();
            var users = await _dataLogic.GetUsersByQuery(query);
            response.Users = Mapper.Map<List<UserResponseItem>>(users);
            return response;
        }

        public async Task<Users> CreateUser(Users user)
        {
            user.Key = Guid.NewGuid().ToString();
            await _dataLogic.AddUser(user);
            Task.Run(() => UpdateUserAccountKey(user));
            //Task.Run(() => UpdateUserAccountKey(user));
            return user;
        }

        private void UpdateUserAccountKey(Users user)
        {
            var response = _accountKeyLogic.GetAccountKey(new AccountKeyRequest() { Email = user.Email, Key = user.Key });
            _dataLogic.UpdateUserAccountKey(user.Id, response.AccountKey);
        }

        public async Task<GetUsersResponse> CreateRandomUsers(int numUsers)
        {
            var response = new GetUsersResponse();
            var result = new List<Users>();
            for (int i = 0; i < numUsers; i++)
            {
                var user = new Users()
                {
                    Email = string.Format("{0}.{1}@{2}.com", GetRandomName(), GetRandomName(), GetRandomEmailProvider()).ToLower(),
                    FullName = string.Format("{0} {1}", GetRandomName(), GetRandomName()),
                    PhoneNumber = GetRandomPhoneNumber(),
                    Password = GetRandomPassword(),
                    MetaData = string.Format("{0}, {1}, {2}", GetRandomSex(), GetRandomAge(), GetRandomPlace())
                };

                await CreateUser(user);
                result.Add(user);
            }
            response.Users = Mapper.Map<List<UserResponseItem>>(result);
            return response;
        }

        private string GetRandomEmailProvider()
        {
            return _emailProviders[_random.Next(0, _emailProviders.Length)];
        }

        private string GetRandomName()
        {
            return _names[_random.Next(0, _names.Length)];
        }

        private string GetRandomAge()
        {
            var i = _random.Next(1, 95);
            return "age " + i;
        }

        private string GetRandomSex()
        {
            var i = _random.Next(0, 2);
            return i == 0 ? "male" : "female";
        }

        private string GetRandomPlace()
        {
            return _places[_random.Next(0, _places.Length)];
        }

        private string GetRandomPhoneNumber()
        {
            var result =_random.Next(1,9).ToString();

            for (int i = 0; i < 9; i++)
            {
                result += _random.Next(0, 10);
            }

            return result;
        }

        private string GetRandomPassword()
        {
            var bytes = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                bytes[i] = (byte)_random.Next(48, 90 + 1);
            }
            return Encoding.ASCII.GetString(bytes);
        }

    }
}
