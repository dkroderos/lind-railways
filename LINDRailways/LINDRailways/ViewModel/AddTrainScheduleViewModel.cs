using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LINDRailways.Model;
using LINDRailways.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace LINDRailways.ViewModel
{
    public partial class AddTrainScheduleViewModel : BaseViewModel
    {
        public AddTrainScheduleViewModel()
        {
            Title = "Add Train Schedule";
        }

        [ObservableProperty]
        public string trainName;

        [ObservableProperty]
        public string trainClass;

        [ObservableProperty]
        public string price;

        [ObservableProperty]
        public string description;

        [ObservableProperty]
        public string photo;

        [ObservableProperty]
        public string origin;

        [ObservableProperty]
        public string destination;

        [ObservableProperty]
        private DateTime departureDate = DateTime.Now.AddDays(1);

        [ObservableProperty]
        private DateTime minimumDepartureDate = DateTime.Now.AddDays(1);

        [ObservableProperty]
        private DateTime maximumDepartureDate = DateTime.Now.AddDays(14);

        [ObservableProperty]
        private DateTime departureTime;

        [RelayCommand]
        private async Task AddTrainScheduleAsync()
        {
            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert(
                "Create Train Schedule", "Are you sure you want to add this schedule?", "Yes", "No");

            if (!isConfirmed)
                return;

            try
            {
                await TrainScheduleService.AddTrainSchedule(new TrainSchedule
                {
                    TrainName = TrainName,
                    TrainClass = TrainClass,
                    Price = int.Parse(Price),
                    Photo = Photo,
                    Origin = Origin,
                    Destination = Destination,
                    DepartureDate = DateOnly.FromDateTime(DepartureDate).ToString(),
                    DepartureTime = TimeOnly.FromDateTime(DepartureTime).ToString(),
                });

                await Shell.Current.CurrentPage.DisplayAlert("Success!",
                    $"Train schedule added", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to add account: {ex.Message}", "OK");
            }
        }
    }
}
