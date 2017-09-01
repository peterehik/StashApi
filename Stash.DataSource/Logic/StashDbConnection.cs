using System;
using System.Data.SQLite;

namespace Stash.DataSource.Logic
{
    internal class StashDbConnection : IDisposable
    {
        private SQLiteConnection _dbConnection = new SQLiteConnection("Data Source=StashDatabase.sqlite");

        public SQLiteConnection Database { get { return _dbConnection; } }

        public StashDbConnection()
        {
            SQLiteConnection.CreateFile("StashDatabase.sqlite");
            _dbConnection.Open();
            CreateUserTable();
        }

        private void CreateUserTable()
        {
            const string sql =
@"create table [IF NOT EXISTS] Users (
	id INTEGER primary key AUTOINCREMENT, 
	email varchar(200) not null, 
	phone_number varchar(20) not null, 
	full_name varchar(200) not null,
	password varchar(100) not null,
	key varchar(100) not null,
	account_key varchar(100) null,
	metadata varchar(2000) not null,
    timestamp DATE DEFAULT (datetime('now','localtime'))
);";

            var command = new SQLiteCommand(sql, _dbConnection);
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            _dbConnection.Dispose();
        }
    }
}
