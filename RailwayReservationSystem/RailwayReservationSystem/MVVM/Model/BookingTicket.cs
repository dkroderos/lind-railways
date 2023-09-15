using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private bool isOneWay;

        public string PassengerNameRecord
        {
            get { return this.passengerNameRecord; }
        }
        public string SeatNumber
        {
            get { return this.seatNumber; }

        }

        public string Origin
        {
            get { return this.origin; }
        }

        public string Destiniation
        {
            get { return this.destiniation; }
        }

        public DateTime DepartureDate
        {
            get { return this.departureDate; }
        }

        public bool IsOneWay
        {
            get { return this.isOneWay; }
        }

        public BookingTicket(string passengerNameRecord, string seatNumber, string origin, string destiniation, DateTime departureDate, bool isOneWay)
        {
            this.passengerNameRecord = passengerNameRecord;
            this.seatNumber = seatNumber;
            this.origin = origin;
            this.destiniation = destiniation;
            this.departureDate = departureDate;
            this.isOneWay = isOneWay;
        }

    
    }
}
