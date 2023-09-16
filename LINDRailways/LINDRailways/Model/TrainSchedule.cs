﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.Model
{
    public class TrainSchedule
    {
        public string TrainName { get; }
        public string Origin { get; }
        public string Destination { get; }
        public TimeOnly DepartureTime { get; }

        public TrainSchedule(string trainName, string origin, string destination, TimeOnly departureTime)
        {
            TrainName = trainName;
            Origin = origin;
            Destination = destination;
            DepartureTime = departureTime;
        }
    }
}
