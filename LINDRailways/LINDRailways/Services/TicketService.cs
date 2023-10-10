using LINDRailways.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.Services
{
    public class TicketService
    {
        private static SQLiteAsyncConnection Database;

        private static async Task Init()
        {
            if (Database is not null)
                return;

            string databaseFilename = "Tickets";
            string databasePath = Path.Combine(FileSystem.AppDataDirectory, databaseFilename);

            SQLite.SQLiteOpenFlags flags =
                SQLite.SQLiteOpenFlags.ReadWrite |
                SQLite.SQLiteOpenFlags.Create |
                SQLite.SQLiteOpenFlags.SharedCache;

            Database = new SQLiteAsyncConnection(databasePath, flags);

            await Database.CreateTableAsync<Ticket>();
        }

        public static async Task AddTicketAsync(Ticket ticket)
        {
            await Init();

            await Database.InsertAsync(ticket);
        }

        public static async Task RemoveTicketAsync(int id)
        {
            await Init();

            await Database.DeleteAsync<Ticket>(id);
        }

        public static async Task<Ticket> GetTicketAsync(int id)
        {
            await Init();

            var ticket = await Database.GetAsync<Ticket>(id);

            return ticket;
        }

        public static async Task<IEnumerable<Ticket>> GetTicketsAsync()
        {
            await Init();

            var ticket = await Database.Table<Ticket>().ToListAsync();

            return ticket;
        }

        public static async Task UpdateTicketAsync(Ticket ticket)
        {
            await Init();

            await Database.UpdateAsync(ticket);
        }

    }
}
