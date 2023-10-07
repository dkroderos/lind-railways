using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.Model
{
    public class TrainScheduleOld
    {
        public TransportationEntity TrainName { get; set; }
        public TransportationEntity Origin { get; set; }
        public TransportationEntity Destination { get; set; }
        public TimeOnly DepartureTime { get; set; }

        public TrainScheduleOld(TransportationEntity trainName, 
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
