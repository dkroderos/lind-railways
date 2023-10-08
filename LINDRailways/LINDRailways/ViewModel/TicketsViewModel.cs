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
    public partial class TicketOldsViewModel : BaseViewModel
    {
        public ObservableCollection<TicketOld> TicketOlds { get; } = new();
        public TicketOldsViewModel()
        {
            Title = "TicketOlds";

            _ = GetTicketOldsAsync();
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
        private async Task GoToTrainScheduleOldsAsync()
        {
            await Shell.Current.GoToAsync(nameof(TrainScheduleOldsPage), true);
        }

        [RelayCommand]
        private async Task GetTicketOldsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                TicketOlds.Clear();

                IEnumerable<TicketOld> allTicketOlds = await TicketOldService.GetAllTicketOlds();

                var paidTicketOlds = from TicketOld in allTicketOlds
                                  where TicketOld.IsPaid == 1 
                                  select TicketOld;

                foreach (TicketOld TicketOld in paidTicketOlds)
                {
                    TicketOlds.Add(TicketOld);
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
    }
}
