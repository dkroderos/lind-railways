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
                await AccountService.RemoveAccountAsync(Account.Username);

                await Shell.Current.CurrentPage.DisplayAlert("Success!",
                    $"Account removed", "OK");

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to delete account: {ex.Message}", "OK");
            }
        } 

        [RelayCommand]
        private async Task Add500ToBalance()
        {
            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert("Add $500 to balance", "I don't even think you don't want to",
                "Add", "Don't Add");

            if (!isConfirmed)
                return;

            try
            {
                var account = await AccountService.GetAccountAsync(Account.Username);

                account.Balance += 500;

                await AccountService.UpdateAccount(account);

                await Shell.Current.CurrentPage.DisplayAlert("Success!",
                    $"Added $500 to balance", "OK");

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
