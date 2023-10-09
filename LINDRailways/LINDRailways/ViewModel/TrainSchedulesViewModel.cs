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
    public partial class TrainSchedulesViewModel : BaseViewModel
    {
        public ObservableCollection<TrainSchedule> TrainSchedules { get; } = new();
        public TrainSchedulesViewModel()
        {
            Title = "Train Schedules";

            _ = GetTrainSchedulesAsync();
        }

        [ObservableProperty]
        private bool isRefreshing;


        [RelayCommand]
        private async Task GetTrainSchedulesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                TrainSchedules.Clear();
                var accounts = await TrainScheduleService.GetTrainSchedulesAsync();

                foreach (TrainSchedule trainSchedule in TrainSchedules)
                {
                    TrainSchedules.Add(trainSchedule);
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
        private async Task GoToAddTrainScheduleAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(AddTrainSchedulePage)}", true);
        }

    }
}
