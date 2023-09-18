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

            AddTrainSchedules();
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

        private void AddTrainSchedules()
        {
            TransportationEntity mugenTrain = new("Mugen Train",
                ".", "mugen_train.jpg", ".");
            TransportationEntity capitolTrain = new("Capitol Train",
                ".", "capitol_train.jpg", ".");
            TransportationEntity seaTrain = new("Sea Train",
                ".", "sea_train.jpg", ".");

            TransportationEntity philippines = new("Philippines",
                ".", ".", ".");
            TransportationEntity japan = new("Japan",
                ".", ".", ".");
            TransportationEntity district12 = new("District 12",
                ".", ".", ".");
            TransportationEntity capitol = new("Capitol",
                ".", ".", ".");
            TransportationEntity water7 = new("Water 7",
                ".", ".", ".");
            TransportationEntity eniesLobby = new("Enies Lobby",
                ".", ".", ".");

            TrainSchedules.Add(new TrainSchedule(mugenTrain, philippines, japan, new TimeOnly(18, 0)));
            TrainSchedules.Add(new TrainSchedule(mugenTrain, japan, philippines, new TimeOnly(6, 0)));
            TrainSchedules.Add(new TrainSchedule(capitolTrain, district12, capitol, new TimeOnly(10, 0)));
            TrainSchedules.Add(new TrainSchedule(capitolTrain, capitol, district12, new TimeOnly(22, 0)));
            TrainSchedules.Add(new TrainSchedule(seaTrain, water7, eniesLobby, new TimeOnly(23, 0)));
            TrainSchedules.Add(new TrainSchedule(seaTrain, eniesLobby, water7, new TimeOnly(11, 0)));
        }
    }
}
