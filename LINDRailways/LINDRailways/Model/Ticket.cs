using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.Model
{
    public class Ticket
    {
        public string PassengerName { get; set; }
        public DateOnly DepartureDate { get; set; }
        public TrainSchedule TrainSchedule { get; set; }
        public bool IsMale { get; set; }
        public bool IsPaid { get; set; }

        public Ticket(string passengerName, DateOnly departureDate, TrainSchedule trainSchedule, bool isMale, bool isPaid)
        {
            this.PassengerName = passengerName;
            this.DepartureDate = departureDate;
            this.TrainSchedule = trainSchedule;
            this.IsMale = isMale;
            this.IsPaid = isPaid;
        }
    }
}
