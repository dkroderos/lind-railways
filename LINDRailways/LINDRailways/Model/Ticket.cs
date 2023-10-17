using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.Model
{
    public class Ticket
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public string AccountUsername { get; set; }
        [Indexed]
        public int ScheduleId { get; set; }
        public string IsPaid { get; set; }
        public string IsBook { get; set; }
        public string PassengerName { get; set; }
        public string Gender { get; set; }
        public string Photo { get; set; }
    }
}
