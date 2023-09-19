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
        Ticket ticket;

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
                    "Ticket Cancelled", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to cancel ticket: {ex.Message}", "OK");
            }

        }
    }
}
