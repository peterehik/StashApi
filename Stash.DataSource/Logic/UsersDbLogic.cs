using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading;
using System.Threading.Tasks;
using Stash.DataSource.Entities;

namespace Stash.DataSource.Logic
{
    public class UsersDbLogic
    {
        private readonly StashDbConnection _db = new StashDbConnection();

        public async Task InsertUser(Users user, CancellationToken token = default(CancellationToken))
        {
            const string query = "insert into Users (email, phone_number, full_name, password, key, account_key, metadata) " +
                           "values (?, ?, ?, ?, ?, ?, ?) ";

            var command = new SQLiteCommand {CommandText = query};
            command.Parameters.Add(user.Email);
            command.Parameters.Add(user.PhoneNumber);
            command.Parameters.Add(user.FullName);
            command.Parameters.Add(user.Password);
            command.Parameters.Add(user.Key);
            command.Parameters.Add(user.AccountKey);
            command.Parameters.Add(user.MetaData);

            await command.ExecuteNonQueryAsync(token);

        }

        public async Task<List<Users>> GetAll(CancellationToken token = new CancellationToken())
        {
            var sql = @"select email as Email, phone_number as PhoneNumber, 
                            full_name as FullName, password, key, account_key as AccountKey, metadata, id 
                            from Users order by timestamp desc";
            var command = new SQLiteCommand(sql);

            var result = new List<Users>();

            var reader = await command.ExecuteReaderAsync(token);
            //while (reader.Read())
            //{
            //    result.Add(reader.GetValues(new object[100]));
            //}
            return null;

        }
    }
}
