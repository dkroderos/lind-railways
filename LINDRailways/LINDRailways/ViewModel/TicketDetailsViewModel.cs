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
                if (Ticket.IsPaid == 1)
                {
                    account.Balance += (int)(Ticket.IsBook == 1 ? trainSchedule.Price * 0.8 : trainSchedule.Price);
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
            if (Ticket.IsPaid == 1)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Already Paid", "Ticket already paid", "OK");
            }

            Account account = await AccountService.GetAccountAsync(Ticket.AccountUsername);
            TrainSchedule trainSchedule = await TrainScheduleService.GetTrainScheduleAsync(Ticket.ScheduleId);

            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert("Pay Ticket", $"Pay ${trainSchedule.Price} to {account.Username}?",
                            "Pay", "Don't Pay");

            if (!isConfirmed)
                return;

            try
            {
                account.Balance -= trainSchedule.Price;

                await AccountService.UpdateAccountAsync(account);

                Ticket.IsPaid = 1;

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
