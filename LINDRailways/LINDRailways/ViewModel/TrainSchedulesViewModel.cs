﻿using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class TrainScheduleOldsViewModel : BaseViewModel
    {
        public ObservableCollection<TrainScheduleOld> TrainScheduleOlds { get; } = new();
        public TrainScheduleOldsViewModel()
        {
            Title = "Train Schedules";

            AddTrainScheduleOlds();
        }

        [ObservableProperty]
        private bool isRefreshing;

        [RelayCommand]
        private async Task GoToAddTicketOldAsync(TrainScheduleOld TrainScheduleOld)
        {
            if (TrainScheduleOld is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(AddTicketOldPage)}",
                true, new Dictionary<string, object>
                {
                    { "TrainScheduleOld", TrainScheduleOld }
                });
        }

        [RelayCommand]
        private async Task GetTrainScheduleOldsAsync()
        {
            if (IsBusy) 
                return;

            try
            {
                IsBusy = true;
                TrainScheduleOlds.Clear();

                AddTrainScheduleOlds();
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

        private void AddTrainScheduleOlds()
        {
            TransportationEntity mugenTrain = new("Mugen Train",
                "A train that is completely safe from demons.", 
                "mugen_train.jpg", ".");
            TransportationEntity capitolTrain = new("Capitol Train",
                "A train to find your love one before your death.", 
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

            TrainScheduleOlds.Add(new TrainScheduleOld(mugenTrain, philippines, japan, new TimeOnly(18, 0)));
            TrainScheduleOlds.Add(new TrainScheduleOld(mugenTrain, japan, philippines, new TimeOnly(6, 0)));
            TrainScheduleOlds.Add(new TrainScheduleOld(capitolTrain, district12, capitol, new TimeOnly(10, 0)));
            TrainScheduleOlds.Add(new TrainScheduleOld(capitolTrain, capitol, district12, new TimeOnly(22, 0)));
            TrainScheduleOlds.Add(new TrainScheduleOld(seaTrain, water7, eniesLobby, new TimeOnly(23, 0)));
            TrainScheduleOlds.Add(new TrainScheduleOld(seaTrain, eniesLobby, water7, new TimeOnly(11, 0)));
        }
    }
}
