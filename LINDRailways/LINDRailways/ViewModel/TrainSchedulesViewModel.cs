using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LINDRailways.Model;
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
        public ObservableCollection<TrainSchedule> TrainSchedule { get; } = new();
        public TrainSchedulesViewModel()
        {
            Title = "Train Schedules";

        }

        [ObservableProperty]
        private bool isRefreshing;

        //private async Task GoToAddTicketOldAsync(TrainScheduleOld TrainScheduleOld)
        //{
        //    if (TrainScheduleOld is null)
        //        return;

        //    await Shell.Current.GoToAsync($"{nameof(AddTicketOldPage)}",
        //        true, new Dictionary<string, object>
        //        {
        //            { "TrainScheduleOld", TrainScheduleOld }
        //        });
        //}
    }

}
