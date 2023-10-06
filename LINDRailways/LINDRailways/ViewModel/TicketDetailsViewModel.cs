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
            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert(
                "Cancel Ticket", "Are you sure you want to cancel this ticket?",
                "Yes", "No");

            if (!isConfirmed)
                return;

            try
            {
                await TicketService.RemoveTicket(Ticket.Id);

                await Shell.Current.CurrentPage.DisplayAlert("Success!",
                    "Ticket Cancelled, Added $60 from your account", "OK");

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to cancel ticket: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task PayTicketAsync()
        {
            if (this.Ticket.IsPaid == 1)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Already Paid!",
                    "Ticket is already paid", "OK");

                return;
            }

            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert(
                "Pay Ticket", "Are you sure you want to pay this ticket?",
                "Yes", "No");

            if (!isConfirmed)
                return;

            try
            {
                await TicketService.PayTicket(Ticket);

                await Shell.Current.CurrentPage.DisplayAlert("Success!",
                    "Ticket Paid, Removed $100 from your balance", "OK");

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to pay ticket: {ex.Message}", "OK");
            }
        }
    }
}
