using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LINDRailways.Model;
using LINDRailways.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LINDRailways.ViewModel
{
    [QueryProperty("TrainScheduleOld", "TrainScheduleOld")]
    public partial class AddTicketOldViewModel : BaseViewModel
    {
        public AddTicketOldViewModel()
        {
        }

        [ObservableProperty]
        private string passengerName;

        [ObservableProperty]
        public string passengerEmail;

        [ObservableProperty]
        private bool isMale;

        [ObservableProperty]
        private TrainScheduleOld trainScheduleOld;

        [ObservableProperty]
        private DateTime departureDate = DateTime.Now.AddDays(1);

        [ObservableProperty]
        private DateTime minimumDepartureDate = DateTime.Now.AddDays(1);

        [ObservableProperty]
        private DateTime maximumDepartureDate = DateTime.Now.AddDays(14);

        [RelayCommand]
        private async Task BookTicketOldAsync()
        {
            if (PassengerName == null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Empty Name",
                    "Please enter your name", "OK");

                return;
            }

            string emailValidationPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(PassengerEmail, emailValidationPattern))
            {

                await Shell.Current.CurrentPage.DisplayAlert("Invalid Email",
                    "Please enter a valid email address", "OK");

                return;
            }

            if (DepartureDate < DateTime.Now)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Departure Date",
                    "Please enter a valid departure date", "OK");

                return;
            }

            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert(
                "Book TicketOld", "Are you sure you want to book?", "Yes", "No");

            if (!isConfirmed)
                return;

            try
            {
                await TicketOldService.AddTicketOld(PassengerName, PassengerEmail, IsMale ? 1 : 0, 1,
                    DateOnly.FromDateTime(DepartureDate).ToString(),
                    TrainScheduleOld.TrainName.Name, TrainScheduleOld.Origin.Name,
                    TrainScheduleOld.Destination.Name, TrainScheduleOld.DepartureTime.ToString()); ;

                await Shell.Current.CurrentPage.DisplayAlert("Success!",
                    "TicketOld Booked, Removed $80 from your balance", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to book TicketOld: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task ReserveTicketOldAsync()
        {
            if (PassengerName == null)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Empty Name",
                    "Please enter your name", "OK");

                return;
            }

            string emailValidationPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(PassengerEmail, emailValidationPattern))
            {

                await Shell.Current.CurrentPage.DisplayAlert("Invalid Email",
                    "Please enter a valid email address", "OK");

                return;
            }


            if (DepartureDate < DateTime.Now)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Departure Date",
                    "Please enter a valid departure date", "OK");

                return;
            }

            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert(
                "Reserve TicketOld", "Are you sure you want to reserve?", "Yes", "No");

            if (!isConfirmed)
                return;

            try
            {
                List<TicketOld> TicketOlds = (List<TicketOld>)await TicketOldService.GetAllTicketOlds();

                var sameScheduledTicketOlds = from TicketOld in TicketOlds
                                           where TicketOld.DepartureTime == TrainScheduleOld.DepartureTime.ToString() &&
                    TicketOld.DepartureDate == DateOnly.FromDateTime(DepartureDate).ToString()
                                           select TicketOld;

                int count = sameScheduledTicketOlds.Count();

                if (count > 50)
                {
                    await Shell.Current.CurrentPage.DisplayAlert("Full Capacity",
                    "Train Capacity is full in this schedule", "OK");

                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to get TicketOlds: {ex.Message}", "OK");
            }


            try
            {
                await TicketOldService.AddTicketOld(PassengerName, PassengerEmail, IsMale ? 1 : 0, 0,
                    DateOnly.FromDateTime(DateTime.Now).ToString(),
                    TrainScheduleOld.TrainName.Name, TrainScheduleOld.Origin.Name,
                    TrainScheduleOld.Destination.Name, TrainScheduleOld.DepartureTime.ToString()); ;

                await Shell.Current.CurrentPage.DisplayAlert("Success!",
                    "TicketOld Reserved", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to reserve TicketOld: {ex.Message}", "OK");
            }
        }
    }
}
