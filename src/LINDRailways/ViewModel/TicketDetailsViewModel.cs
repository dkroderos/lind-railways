using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LINDRailways.Model;
using LINDRailways.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.ViewModel
{
    [QueryProperty("Ticket", "Ticket")]
    public partial class TicketDetailsViewModel : BaseViewModel
    {
        public TicketDetailsViewModel()
        {
        }

        [ObservableProperty]
        private Ticket ticket;

        [RelayCommand]
        private async Task CancelTicketAsync()
        {
            Account account = await AccountService.GetAccountAsync(Ticket.AccountUsername);
            TrainSchedule trainSchedule = await TrainScheduleService.GetTrainScheduleAsync(Ticket.ScheduleId);

            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert("Cancel Ticket", "Are you sure you want to cancel this ticket?",
                            "Yes", "No");

            if (!isConfirmed)
                return;

            try
            {
                if (Ticket.IsPaid.Equals("Yes"))
                {
                    //DateTime departureDateTime = DateTime.Parse(trainSchedule.DepartureDate).Add(DateTime.Parse(trainSchedule.DepartureTime).TimeOfDay);
                    //if (DateTime.Now < departureDateTime)
                    //{
                    account.Balance += (int)(Ticket.IsBook.Equals("Yes") ? trainSchedule.Price * 0.7 : trainSchedule.Price * 0.8);
                    //}
                }
                trainSchedule.NumberOfPassengers -= 1;

                await AccountService.UpdateAccountAsync(account);
                await TrainScheduleService.UpdateTrainScheduleAsync(trainSchedule);

                await TicketService.RemoveTicketAsync(Ticket.Id);

                await Shell.Current.CurrentPage.DisplayAlert("Success!",
                    $"Ticket cancelled", "OK");

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to add balance: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task PayTicketAsync()
        {
            TrainSchedule trainSchedule = await TrainScheduleService.GetTrainScheduleAsync(Ticket.ScheduleId);

            //DateTime departureDate = DateTime.ParseExact(trainSchedule.DepartureDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            //DateTime departureTime = DateTime.ParseExact(trainSchedule.DepartureTime, "hh:mm tt", CultureInfo.InvariantCulture);

            //DateTime departureDateTime = departureDate.Date.Add(departureTime.TimeOfDay);

            //if (DateTime.Now > departureDateTime)
            //{
            //    await Shell.Current.CurrentPage.DisplayAlert("Ticket Expired", "Train already departured", "OK");

            //    return;
            //}

            if (Ticket.IsPaid.Equals("Yes"))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Already Paid", "Ticket already paid", "OK");

                return;
            }

            Account account = await AccountService.GetAccountAsync(Ticket.AccountUsername);

            if (account.Balance < trainSchedule.Price)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Insufficient Funds",
                                    $"Ticket costs ${trainSchedule.Price} the the account only has a balance of ${account.Balance}", "OK");

                return;
            }

            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert("Pay Ticket", $"Pay ${trainSchedule.Price} to {account.Username}?",
                            "Pay", "Don't Pay");

            if (!isConfirmed)
                return;

            try
            {
                account.Balance -= trainSchedule.Price;
                Ticket.IsPaid = "Yes";

                await AccountService.UpdateAccountAsync(account);
                await TicketService.UpdateTicketAsync(Ticket);

                await Shell.Current.CurrentPage.DisplayAlert("Success!",
                    $"Paid ${trainSchedule.Price} to {account.Username}", "OK");

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to add balance: {ex.Message}", "OK");
            }
        }
    }
}
