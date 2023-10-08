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
    [QueryProperty("Account", "Account")]
    public partial class AccountDetailsViewModel : BaseViewModel
    {
        public AccountDetailsViewModel()
        {
            Title = Account.Name;
        }

        [ObservableProperty]
        private Account account;

        [RelayCommand]
        private async Task DeleteAccountAsync()
        {
            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert("Remove Account", "Are you sure you want to remove this account?",
                "Yes", "No");

            if (!isConfirmed)
                return;

            try
            {
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to cancel TicketOld: {ex.Message}", "OK");
            }
        }
    }
}
