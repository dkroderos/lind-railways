using LINDRailways.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.Services
{
    public static class TicketService
    {
        private static SQLiteAsyncConnection Database;
        private static async Task Init()
        {
            if (Database is not null)
                return;

            string databaseFilename = "Tickets.db3";

            SQLite.SQLiteOpenFlags flags =
                SQLite.SQLiteOpenFlags.ReadWrite |
                SQLite.SQLiteOpenFlags.Create |
                SQLite.SQLiteOpenFlags.SharedCache; 

            Database = new SQLiteAsyncConnection(databaseFilename, flags);

            var result = await Database.CreateTableAsync<Ticket>();
        }

        public static async Task AddTicket(string passengerName, DateOnly departureDate,
            bool isMale, bool isPaid, TrainSchedule trainSchedule)
        {
            await Init();

            var ticket = new Ticket
            {
                PassengerName = passengerName,
                DepartureDate = departureDate,
                IsMale = isMale,
                IsPaid = isPaid,
                TrainSchedule = trainSchedule
            };

            var id = await Database.InsertAsync(ticket);
        }

        public static async Task RemoveTicket(int id)
        {
            await Init();

            await Database.DeleteAsync<Ticket>(id);
        }

        public static async Task<IEnumerable<Ticket>> GetPaidTickets()
        {
            await Init();

            var ticket = await Database.Table<Ticket>().ToListAsync();

            return ticket;
        }
    }
}
