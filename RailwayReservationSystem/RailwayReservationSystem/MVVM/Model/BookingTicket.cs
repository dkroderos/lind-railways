using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayReservationSystem.MVVM.Model
{
    public class BookingTicket
    {
        private string passengerNameRecord;
        private string seatNumber;
        private string origin;
        private string destiniation;
        private DateTime departureDate;

        public string PassengerNameRecord
        {
            get { return this.passengerNameRecord; }
        }
        public string SeatNumber
        {
            get { return this.seatNumber; }
        }

        public BookingTicket(string passengerNameRecord, string seatNumber, string origin, string destiniation, DateTime departureDate)
        {
            this.passengerNameRecord = passengerNameRecord;
            this.seatNumber = seatNumber;
            this.origin = origin;
            this.destiniation = destiniation;
            this.departureDate = departureDate;
        }
    }
}
