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
    public partial class AccountsViewModel : BaseViewModel
    {
        public ObservableCollection<Account> Accounts { get; } = new();
        public AccountsViewModel()
        {
            Title = "Accounts";

            _ = GetAccountsAsync();
        }

        [ObservableProperty]
        private bool isRefreshing;


        [RelayCommand]
        private async Task GetAccountsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                Accounts.Clear();
                var accounts = await AccountService.GetAccountsAsync();

                foreach (Account account in accounts)
                {
                    Accounts.Add(account);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get accounts: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
        
        [RelayCommand]
        private async Task GoToAccountDetails(Account account)
        {
            if (account is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(AccountDetailsPage)}",
                true, new Dictionary<string, object>
                {
                    { "Account", account }
                });
        }

        [RelayCommand]
        private async Task GoToAddAccountAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(AddAccountPage)}");
        }
    }
}
