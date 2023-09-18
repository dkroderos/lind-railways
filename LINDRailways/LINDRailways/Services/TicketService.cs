using LINDRailways.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.Services
{
    public static class TicketService
    {

        private static SQLiteAsyncConnection db;
        private static async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TicketsData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Ticket>();
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
        }

        public static async Task RemoveTicket(int id)
        {
            await Init();

            await db.DeleteAsync<Ticket>(id);
        }

        public static async Task<IEnumerable<Ticket>> GetPaidTicket()
        {
            await Init();

            var ticket = await db.Table<Ticket>().ToListAsync();

            return ticket;
        }
    }
}
