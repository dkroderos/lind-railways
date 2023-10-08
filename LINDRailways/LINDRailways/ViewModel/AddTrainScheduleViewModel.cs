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
            if (TrainName is null || TrainName.Equals(""))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Train Name",
                    "Please enter a valid train name", "OK");

                return;
            }

            if (TrainClass is null || TrainClass.Equals(""))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Train Class",
                    "Please enter a valid train class", "OK");

                return;
            }

            if (Price is null || Price.Equals("") || !int.TryParse(Price, out _))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Price",
                    "Please enter a valid price", "OK");

                return;
            }

            if (Photo is null || Photo.Equals(""))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Train Photo",
                    "Please enter a valid train photo", "OK");

                return;
            }

            if (Origin is null || Origin.Equals(""))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Train Origin",
                    "Please enter a valid train origin", "OK");

                return;
            }

            if (Destination is null || Destination.Equals(""))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Train Destination",
                    "Please enter a valid train destination", "OK");

                return;
            }

            if (DepartureDate < DateTime.Now)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Departure Date",
                    "Please enter a valid departure date", "OK");

                return;
            }

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
