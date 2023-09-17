using CommunityToolkit.Mvvm.Input;
using LINDRailways.Model;
using LINDRailways.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.ViewModel
{
    public partial class TrainSchedulesViewModel : BaseViewModel
    {
        public ObservableCollection<TrainSchedule> TrainSchedules { get; } = new();
        public TrainSchedulesViewModel()
        {
            Title = "Train Schedules";

            TrainSchedules.Add(new TrainSchedule("Mugen Train", "Philippines",
                "Japan", new TimeOnly(18, 0)));
            TrainSchedules.Add(new TrainSchedule("Mugen Train", "Japan",
                "Philippines", new TimeOnly(6, 0)));
            TrainSchedules.Add(new TrainSchedule("Capitol Train", "District 12",
                "Capitol", new TimeOnly(10, 0)));
            TrainSchedules.Add(new TrainSchedule("Capitol Train", "Capitol",
                "District 12", new TimeOnly(22, 0)));
            TrainSchedules.Add(new TrainSchedule("Sea Train", "Water 7",
                "Enies Lobby", new TimeOnly(23, 0)));
            TrainSchedules.Add(new TrainSchedule("Sea Train", "Enies Lobby",
                "Water 7", new TimeOnly(11, 0)));
        }

        [RelayCommand]
        private async Task GoToAddTicketAsync(TrainSchedule trainSchedule)
        {
            if (trainSchedule is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(AddTicketPage)}",
                true, new Dictionary<string, object>
                {
                    { "TrainSchedule", trainSchedule } 
                });
        }
    }
}
