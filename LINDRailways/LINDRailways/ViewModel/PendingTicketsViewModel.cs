using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LINDRailways.Model;
using LINDRailways.Services;
using LINDRailways.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.ViewModel
{
    public partial class PendingTicketsViewModel : BaseViewModel
    {
        public ObservableCollection<Ticket> PendingTickets { get; } = new();
        public PendingTicketsViewModel()
        {
            Title = "Pending Tickets";

            _ = GetPendingTicketsAsync();
        }

        [ObservableProperty]
        private bool isRefreshing;

        [RelayCommand]
        private async Task GoToTicketDetailsAsync(Ticket ticket)
        {
            if (ticket is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(TicketDetailsPage)}",
                true, new Dictionary<string, object>
                {
                    { "Ticket", ticket }
                });
        }

        [RelayCommand]
        private async Task GetPendingTicketsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                PendingTickets.Clear();

                IEnumerable<Ticket> allTickets = await TicketService.GetAllTickets();

                var pendingTickets = from ticket in allTickets
                                     where ticket.IsPaid == 0
                                     select ticket;

                foreach (Ticket ticket in pendingTickets)
                {
                    PendingTickets.Add(ticket);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get tickets: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        private async Task CancelAllTicketsAsync()
        {
            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert(
                            "Cancel ALL Tickets", "Are you sure you want to cancel ALL tickets", "Yes", "No");

            if (!isConfirmed)
                return;

            bool isConfirmedTwiceReversed = await Shell.Current.CurrentPage.DisplayAlert(
                            "DOUBLE CHECK", "Are you sure you want to cancel ALL tickets", "No", "Yes");

            if (isConfirmedTwiceReversed)
                return;

            try
            {
                foreach (Ticket pendingTicket in PendingTickets)
                    await TicketService.RemoveTicket(pendingTicket.Id);

                int refundPrice = PendingTickets.Count * 60;

                PendingTickets.Clear();

                await Shell.Current.CurrentPage.DisplayAlert("Cancelled!",
                                   "ALL pending tickets has been canceled, Added $" + refundPrice + " from your account", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to cancel all pending tickets: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task PayAllTicketsAsync()
        {
            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert(
                            "Pay ALL Tickets", "Are you sure you want to pay ALL tickets", "Yes", "No");

            if (!isConfirmed)
                return;

            bool isConfirmedTwiceReversed = await Shell.Current.CurrentPage.DisplayAlert(
                            "DOUBLE CHECK", "Are you sure you want to pay ALL tickets", "No", "Yes");

            if (isConfirmedTwiceReversed)
                return;

            try
            {
                foreach (Ticket pendingTicket in PendingTickets)
                    await TicketService.PayTicket(pendingTicket);

                int price = PendingTickets.Count * 100;

                PendingTickets.Clear();

                await Shell.Current.CurrentPage.DisplayAlert("Paid!",
                                   "ALL pending tickets has been paid, Removed" + price + " from your balance", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to pay all pending tickets: {ex.Message}", "OK");
            }
        }
    }
}
