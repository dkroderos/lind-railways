using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.Model
{
    public class TrainSchedule
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string TrainClass { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string TrainImage { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string DepartureTime { get; set; }
        public string DepartureDate { get; set; }
    }
}
