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
    public partial class PendingTicketOldsViewModel : BaseViewModel
    {
        public ObservableCollection<TicketOld> PendingTicketOlds { get; } = new();
        public PendingTicketOldsViewModel()
        {
            Title = "Pending TicketOlds";

            _ = GetPendingTicketOldsAsync();
        }

        [ObservableProperty]
        private bool isRefreshing;

        [RelayCommand]
        private async Task GoToTicketOldDetailsAsync(TicketOld TicketOld)
        {
            if (TicketOld is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(TicketOldDetailsPage)}",
                true, new Dictionary<string, object>
                {
                    { "TicketOld", TicketOld }
                });
        }

        [RelayCommand]
        private async Task GetPendingTicketOldsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                PendingTicketOlds.Clear();

                IEnumerable<TicketOld> allTicketOlds = await TicketOldService.GetAllTicketOlds();

                var pendingTicketOlds = from TicketOld in allTicketOlds
                                     where TicketOld.IsPaid == 0
                                     select TicketOld;

                foreach (TicketOld TicketOld in pendingTicketOlds)
                {
                    PendingTicketOlds.Add(TicketOld);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get TicketOlds: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        private async Task CancelAllTicketOldsAsync()
        {
            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert(
                            "Cancel ALL TicketOlds", "Are you sure you want to cancel ALL TicketOlds", "Yes", "No");

            if (!isConfirmed)
                return;

            bool isConfirmedTwiceReversed = await Shell.Current.CurrentPage.DisplayAlert(
                            "DOUBLE CHECK", "Are you sure you want to cancel ALL TicketOlds", "No", "Yes");

            if (isConfirmedTwiceReversed)
                return;

            try
            {
                foreach (TicketOld pendingTicketOld in PendingTicketOlds)
                    await TicketOldService.RemoveTicketOld(pendingTicketOld.Id);

                int refundPrice = PendingTicketOlds.Count * 60;

                PendingTicketOlds.Clear();

                await Shell.Current.CurrentPage.DisplayAlert("Cancelled!",
                                   "ALL pending TicketOlds has been canceled, Added $" + refundPrice + " from your account", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to cancel all pending TicketOlds: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task PayAllTicketOldsAsync()
        {
            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert(
                            "Pay ALL TicketOlds", "Are you sure you want to pay ALL TicketOlds", "Yes", "No");

            if (!isConfirmed)
                return;

            bool isConfirmedTwiceReversed = await Shell.Current.CurrentPage.DisplayAlert(
                            "DOUBLE CHECK", "Are you sure you want to pay ALL TicketOlds", "No", "Yes");

            if (isConfirmedTwiceReversed)
                return;

            try
            {
                foreach (TicketOld pendingTicketOld in PendingTicketOlds)
                    await TicketOldService.PayTicketOld(pendingTicketOld);

                int price = PendingTicketOlds.Count * 100;

                PendingTicketOlds.Clear();

                await Shell.Current.CurrentPage.DisplayAlert("Paid!",
                                   "ALL pending TicketOlds has been paid, Removed" + price + " from your balance", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to pay all pending TicketOlds: {ex.Message}", "OK");
            }
        }
    }
}
