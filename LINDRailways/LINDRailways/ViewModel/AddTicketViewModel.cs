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
    [QueryProperty("TrainSchedule", "TrainSchedule")]
    public partial class AddTicketViewModel : BaseViewModel
    {
        public AddTicketViewModel()
        {
        }

        [ObservableProperty]
        private string passengerName;

        [ObservableProperty]
        public string passengerEmail;

        [ObservableProperty]
        private bool isMale;

        [ObservableProperty]
        private TrainSchedule trainSchedule;

        [ObservableProperty]
        private DateTime departureDate = DateTime.Now.AddDays(1);

        [ObservableProperty]
        private DateTime minimumDepartureDate = DateTime.Now.AddDays(1);

        [ObservableProperty]
        private DateTime maximumDepartureDate = DateTime.Now.AddDays(14);

        [RelayCommand]
        private async Task BookTicketAsync()
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
                "Book Ticket", "Are you sure you want to book?", "Yes", "No");

            if (!isConfirmed)
                return;

            try
            {
                await TicketService.AddTicket(PassengerName, PassengerEmail, IsMale ? 1 : 0, 1,
                    DateOnly.FromDateTime(DepartureDate).ToString(),
                    TrainSchedule.TrainName.Name, TrainSchedule.Origin.Name,
                    TrainSchedule.Destination.Name, TrainSchedule.DepartureTime.ToString()); ;

                await Shell.Current.CurrentPage.DisplayAlert("Success!",
                    "Ticket Booked, Removed $80 from your balance", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to book ticket: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task ReserveTicketAsync()
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
                "Reserve Ticket", "Are you sure you want to reserve?", "Yes", "No");

            if (!isConfirmed)
                return;

            try
            {
                List<Ticket> tickets = (List<Ticket>)await TicketService.GetAllTickets();

                var sameScheduledTickets = from ticket in tickets
                                           where ticket.DepartureTime == TrainSchedule.DepartureTime.ToString() &&
                    ticket.DepartureDate == DateOnly.FromDateTime(DepartureDate).ToString()
                                           select ticket;

                int count = sameScheduledTickets.Count();

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
                    $"Unable to get tickets: {ex.Message}", "OK");
            }


            try
            {
                await TicketService.AddTicket(PassengerName, PassengerEmail, IsMale ? 1 : 0, 0,
                    DateOnly.FromDateTime(DateTime.Now).ToString(),
                    TrainSchedule.TrainName.Name, TrainSchedule.Origin.Name,
                    TrainSchedule.Destination.Name, TrainSchedule.DepartureTime.ToString()); ;

                await Shell.Current.CurrentPage.DisplayAlert("Success!",
                    "Ticket Reserved", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to reserve ticket: {ex.Message}", "OK");
            }
        }
    }
}
