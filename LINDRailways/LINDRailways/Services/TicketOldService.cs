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
    public static class TicketOldService
    {
        private static SQLiteAsyncConnection Database;
        private static async Task Init()
        {
            if (Database is not null)
                return;

            string databaseFilename = "TicketOlds";
            string databasePath = Path.Combine(FileSystem.AppDataDirectory, databaseFilename);

            SQLite.SQLiteOpenFlags flags =
                SQLite.SQLiteOpenFlags.ReadWrite |
                SQLite.SQLiteOpenFlags.Create |
                SQLite.SQLiteOpenFlags.SharedCache;

            Database = new SQLiteAsyncConnection(databasePath, flags);

            await Database.CreateTableAsync<TicketOld>();
        }

        public static async Task AddTicketOld(string passengerName, string passengerEmail, int isMale,
            int isPaid, string departureDate, string trainName, string origin,
            string destination, string departureTime)
        {
            await Init();

            var TicketOld = new TicketOld
            {
                PassengerName = passengerName,
                PassengerEmail = passengerEmail,
                IsMale = isMale,
                IsPaid = isPaid,
                DepartureDate = departureDate,
                TrainName = trainName,
                Origin = origin,
                Destination = destination,
                DepartureTime = departureTime,
                IsPaidString = isPaid == 1 ? "Yes" : "No",
                Gender = isMale == 1 ? "Male" : "Female",
                PassengerPhoto = isMale == 1 ? "male.jpg" : "female.jpg"
            };

            await Database.InsertAsync(TicketOld);
        }

        public static async Task RemoveTicketOld(int id)
        {
            await Init();

            await Database.DeleteAsync<TicketOld>(id);
        }

        public static async Task PayTicketOld(TicketOld TicketOld)
        {
            await Init();

            var newTicketOld = new TicketOld
            {
                PassengerName = TicketOld.PassengerName,
                PassengerEmail = TicketOld.PassengerEmail,
                IsMale = TicketOld.IsMale,
                IsPaid = 1,
                DepartureDate = TicketOld.DepartureDate,
                TrainName = TicketOld.TrainName,
                Origin = TicketOld.Origin,
                Destination = TicketOld.Destination,
                DepartureTime = TicketOld.DepartureTime,
                IsPaidString = "Yes",
                Gender = TicketOld.IsMale == 1 ? "Male" : "Female",
                PassengerPhoto = TicketOld.IsMale == 1 ? "male.jpg" : "female.jpg"
            };

            await Database.InsertAsync(newTicketOld);

            await Database.DeleteAsync<TicketOld>(TicketOld.Id);
        }

        public static async Task<IEnumerable<TicketOld>> GetAllTicketOlds()
        {
            await Init();

            var TicketOld = await Database.Table<TicketOld>().ToListAsync();

            return TicketOld;
        }
    }
}
