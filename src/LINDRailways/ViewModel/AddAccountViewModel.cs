using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LINDRailways.Model;
using LINDRailways.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LINDRailways.ViewModel
{
    public partial class AddAccountViewModel : BaseViewModel
    {
        public AddAccountViewModel()
        {
            Title = "Add Account";
        }

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private int isMale;

        [ObservableProperty]
        private string photo;

        [ObservableProperty]
        private string gender;

        [ObservableProperty]
        private int startingBalance;

        [RelayCommand]
        private async Task AddAccountAsync()
        {
            if (Username is null || Username.Equals(""))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Username",
                    "Please enter your username", "OK");

                return;
            }

            var usernames = from account in await AccountService.GetAccountsAsync()
                            select account.Username;

            if (usernames.Contains(Username))
            {
                await Shell.Current.CurrentPage.DisplayAlert("Invalid Username",
                    "Username already exists", "OK");

                return;
            }

            string emailValidationPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(Email, emailValidationPattern))
            {

                await Shell.Current.CurrentPage.DisplayAlert("Invalid Email",
                    "Please enter a valid email address", "OK");

                return;
            }

            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert(
                "Create Account", "Are you sure you want to add this account?", "Yes", "No");

            if (!isConfirmed)
                return;

            try
            {
                await AccountService.AddAccountAsync(new Account
                {
                    Username = Username,
                    Name = Name,
                    Email = Email,
                    Photo = IsMale == 1 ? "male.jpg" : "female.jpg",
                    Gender = IsMale == 1 ? "Male" : "Female",
                    Balance = 500
                });

                await Shell.Current.CurrentPage.DisplayAlert("Success!",
                    $"{Name} has been added", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to add account: {ex.Message}", "OK");
            }

        }
    }
}
