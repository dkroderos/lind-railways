using LINDRailways.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.Services
{
    public class AccountService
    {
        private static SQLiteAsyncConnection Database;
        private static async Task Init()
        {
            if (Database is not null)
                return;

            string databaseFilename = "Accounts";
            string databasePath = Path.Combine(FileSystem.AppDataDirectory, databaseFilename);

            SQLite.SQLiteOpenFlags flags =
                SQLite.SQLiteOpenFlags.ReadWrite |
                SQLite.SQLiteOpenFlags.Create |
                SQLite.SQLiteOpenFlags.SharedCache;

            Database = new SQLiteAsyncConnection(databasePath, flags);

            await Database.CreateTableAsync<Account>();
        }

        public static async Task AddTicketAsync(Account account)
        {
            await Init();

            await Database.InsertAsync(account);
        }

        public static async Task RemoveTicketAsync(int id)
        {
            await Init();

            await Database.DeleteAsync(id);
        } 
        
        public static async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            await Init();

            var account = await Database.Table<Account>().ToListAsync();

            return account;
        }

    }
}
