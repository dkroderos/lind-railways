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
    [QueryProperty("TicketOld", "TicketOld")]
    public partial class TicketOldDetailsViewModel : BaseViewModel
    {
        public TicketOldDetailsViewModel()
        {
        }

        [ObservableProperty]
        private TicketOld ticketOld;

        [RelayCommand]
        private async Task CancelTicketOldAsync()
        {
            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert(
                "Cancel TicketOld", "Are you sure you want to cancel this TicketOld?",
                "Yes", "No");

            if (!isConfirmed)
                return;

            try
            {
                await TicketOldService.RemoveTicketOld(TicketOld.Id);

                await Shell.Current.CurrentPage.DisplayAlert("Success!",
                    "TicketOld Cancelled, Added $60 from your account", "OK");

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to cancel TicketOld: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task PayTicketOldAsync()
        {
            if (this.TicketOld.IsPaid == 1)
            {
                await Shell.Current.CurrentPage.DisplayAlert("Already Paid!",
                    "TicketOld is already paid", "OK");

                return;
            }

            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert(
                "Pay TicketOld", "Are you sure you want to pay this TicketOld?",
                "Yes", "No");

            if (!isConfirmed)
                return;

            try
            {
                await TicketOldService.PayTicketOld(TicketOld);

                await Shell.Current.CurrentPage.DisplayAlert("Success!",
                    "TicketOld Paid, Removed $100 from your balance", "OK");

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to pay TicketOld: {ex.Message}", "OK");
            }
        }
    }
}
