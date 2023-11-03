using LINDRailways.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.Services
{
    public class TrainScheduleService
    {
        private static SQLiteAsyncConnection Database;

        private static async Task Init()
        {
            if (Database is not null)
                return;

            string databaseFilename = "TrainSchedules";
            string databasePath = Path.Combine(FileSystem.AppDataDirectory, databaseFilename);

            SQLite.SQLiteOpenFlags flags =
                SQLite.SQLiteOpenFlags.ReadWrite |
                SQLite.SQLiteOpenFlags.Create |
                SQLite.SQLiteOpenFlags.SharedCache;

            Database = new SQLiteAsyncConnection(databasePath, flags);

            await Database.CreateTableAsync<TrainSchedule>();
        }

        public static async Task AddTrainSchedule(TrainSchedule trainSchedule)
        {
            await Init();

            await Database.InsertAsync(trainSchedule);
        }

        public static async Task RemoveTrainScheduleAsync(int id)
        {
            await Init();

            await Database.DeleteAsync<TrainSchedule>(id);
        }

        public static async Task<TrainSchedule> GetTrainScheduleAsync(int id)
        {
            await Init();

            var trainSchedule = await Database.GetAsync<TrainSchedule>(id);

            return trainSchedule;
        }

        public static async Task<IEnumerable<TrainSchedule>> GetTrainSchedulesAsync()
        {
            await Init();

            var trainSchedule = await Database.Table<TrainSchedule>().ToListAsync();

            return trainSchedule;
        }

        public static async Task UpdateTrainScheduleAsync(TrainSchedule trainSchedule)
        {
            await Init();

            await Database.UpdateAsync(trainSchedule);
        }
    }
}
