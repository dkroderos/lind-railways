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

namespace LINDRailways.ViewModel
{
    public partial class AddTicketViewModel : BaseViewModel
    {
        public AddTicketViewModel()
        {
            Title = "Add Ticket";
        }

        [ObservableProperty]
        public string accountUsername;

        [ObservableProperty]
        public string scheduleId;

        [ObservableProperty]
        public string initialPaymentMethod;

        [ObservableProperty]
        public string passengerName;

        [ObservableProperty]
        private int isMale;

        [RelayCommand]
        private async Task BookTicketAsync()
        {
            if (AccountUsername is null || AccountUsername.Equals(""))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Username",
                    "Please enter your username", "OK");

                return;
            }

            if (AccountUsername[0] != '@')
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Username",
                    "Username must start with @", "OK");

                return;
            }

            var usernames = from account in await AccountService.GetAccountsAsync()
                            select account.Username;

            if (!usernames.Contains(AccountUsername))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Username",
                    "Username not found", "OK");

                return;
            }

            if (ScheduleId is null || ScheduleId.Equals("") || !int.TryParse(ScheduleId, out _))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Schedule Id",
                    "Please enter a valid schedule id", "OK");

                return;
            }

            var scheduleIds = from schedule in await TrainScheduleService.GetTrainSchedulesAsync()
                              select schedule.Id;

            if (!scheduleIds.Contains(int.Parse(ScheduleId)))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Schdeule Id",
                    "Train schedule not found", "OK");

                return;
            }

            var ticketAccount = await AccountService.GetAccountAsync(AccountUsername);
            var ticketSchedule = await TrainScheduleService.GetTrainScheduleAsync(int.Parse(ScheduleId));

            if (ticketSchedule.NumberOfPassengers >= ticketSchedule.Capacity)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Full Capacity",
                    $"The train is already full", "OK");

                return;
            }

            int bookingPrice = (int)(ticketSchedule.Price * 0.8);

            if (ticketAccount.Balance < bookingPrice)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Insufficient Funds",
                    $"Ticket costs ${bookingPrice} the the account only has a balance of ${ticketAccount.Balance}", "OK");

                return;
            }

            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert(
                "Book Ticket", $"Are you sure you want to book in this ticket? The cost is ${bookingPrice}", "Yes", "No");

            if (!isConfirmed)
                return;

            try
            {
                await TicketService.AddTicketAsync(new Ticket
                {
                    ScheduleId = int.Parse(ScheduleId),
                    AccountUsername = AccountUsername,
                    PassengerName = PassengerName,
                    Gender = IsMale == 1 ? "Male" : "Female",
                    Photo = IsMale == 1 ? "male.jpg" : "female.jpg",
                    IsPaid = "Yes",
                    IsBook = "Yes"
                });

                ticketAccount.Balance -= bookingPrice;
                ticketSchedule.NumberOfPassengers += 1;

                await AccountService.UpdateAccountAsync(ticketAccount);
                await TrainScheduleService.UpdateTrainScheduleAsync(ticketSchedule);

                await Shell.Current.CurrentPage.DisplayAlert("Success!",
                    $"Ticket Booked! Spent ${ticketSchedule.Price * 0.8} to {ticketAccount.Username}", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to add ticket: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task ReserveTicketAsync()
        {
            if (AccountUsername is null || AccountUsername.Equals(""))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Username",
                    "Please enter your username", "OK");

                return;
            }

            var usernames = from account in await AccountService.GetAccountsAsync()
                            select account.Username;

            if (!usernames.Contains(AccountUsername))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Username",
                    "Username not found", "OK");

                return;
            }

            if (ScheduleId is null || ScheduleId.Equals("") || !int.TryParse(ScheduleId, out _))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Schedule Id",
                    "Please enter a valid schedule id", "OK");

                return;
            }

            var scheduleIds = from schedule in await TrainScheduleService.GetTrainSchedulesAsync()
                              select schedule.Id;

            if (!scheduleIds.Contains(int.Parse(ScheduleId)))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Schdeule Id",
                    "Train schedule not found", "OK");

                return;
            }

            var ticketAccount = await AccountService.GetAccountAsync(AccountUsername);
            var ticketSchedule = await TrainScheduleService.GetTrainScheduleAsync(int.Parse(ScheduleId));

            if (ticketSchedule.NumberOfPassengers >= ticketSchedule.Capacity)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Full Capacity",
                    $"The train is already full, We will notify you once a seat is available", "OK");

                return;
            }

            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert(
                "Book Ticket", $"Are you sure you want to reserve in this ticket?", "Yes", "No");

            if (!isConfirmed)
                return;

            try
            {
                await TicketService.AddTicketAsync(new Ticket
                {
                    ScheduleId = int.Parse(ScheduleId),
                    AccountUsername = AccountUsername,
                    PassengerName = PassengerName,
                    Gender = IsMale == 1 ? "Male" : "Female",
                    Photo = IsMale == 1 ? "male.jpg" : "female.jpg",
                    IsPaid = "No",
                    IsBook = "No" 
                });

                ticketSchedule.NumberOfPassengers += 1;

                await TrainScheduleService.UpdateTrainScheduleAsync(ticketSchedule);

                await Shell.Current.CurrentPage.DisplayAlert("Success!",
                    $"Ticket Reserved!", "OK");
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