using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.Model
{
    public class TrainSchedule
    {
        //public string TrainName { get; set; }
        //public string Origin { get; set; }
        //public string Destination { get; set; }
        //public TimeOnly DepartureTime { get; set; }

        //public TrainSchedule(string trainName, string origin, string destination, TimeOnly departureTime)
        //{
        //    TrainName = trainName;
        //    Origin = origin;
        //    Destination = destination;
        //    DepartureTime = departureTime;
        //}

        public TransportationEntity TrainName { get; set; }
        public TransportationEntity Origin { get; set; }
        public TransportationEntity Destination { get; set; }
        public TimeOnly DepartureTime { get; set; }

        public TrainSchedule(TransportationEntity trainName, 
            TransportationEntity origin, TransportationEntity destination, 
            TimeOnly departuretime)
        {
            TrainName = trainName;
            Origin = origin;
            Destination = destination;
            DepartureTime = departuretime;
        }
    }
}
