using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LINDRailways.Model;
using LINDRailways.Services;
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
        private ObservableCollection<Account> Accounts = new();
        public AccountsViewModel()
        {
            Title = "Accounts";
        }

        [ObservableProperty]
        private bool isRefreshing;


        [RelayCommand]
        private async Task GetTrainScheduleOldsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                Accounts.Clear();
                var accounts = await AccountService.GetAllAccountsAsync();

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
    }
}
