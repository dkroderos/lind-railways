using CommunityToolkit.Mvvm.ComponentModel;
using LINDRailways.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.ViewModel
{
    [QueryProperty("TrainSchedule", "TrainSchedule")]
    public partial class AddTicketViewModel : BaseViewModel
    {
        public AddTicketViewModel()
        {
        }

        [ObservableProperty]
        TrainSchedule trainSchedule;
    }
}
