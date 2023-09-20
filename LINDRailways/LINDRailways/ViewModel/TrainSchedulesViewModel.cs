using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LINDRailways.Model;
using LINDRailways.View;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        [ObservableProperty]
        private bool isRefreshing;

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

        [RelayCommand]
        private async Task GetTrainSchedulesAsync()
        {
            if (IsBusy) 
                return;

            try
            {
                IsBusy = true;
                TrainSchedules.Clear();

                AddTrainSchedules();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get train schedules: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        private void AddTrainSchedules()
        {
            TransportationEntity mugenTrain = new("Mugen Train",
                "A train that is completely safe from demons.", 
                "mugen_train.jpg", ".");
            TransportationEntity capitolTrain = new("Capitol Train",
                "A train where you can find your love one before your death.", 
                "capitol_train.jpg", ".");
            TransportationEntity seaTrain = new("Sea Train",
                "Is it a boat? Is it a fish? No, it's a train on water.", 
                "sea_train.jpg", ".");

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
