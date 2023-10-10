using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.ViewModel
{
    public partial class AddTicketViewModel : BaseViewModel
    {
        public AddTicketViewModel()
        {
            Title = "Add Ticket";
        }

        [ObservableProperty]
        public string accountUsername;

        [ObservableProperty]
        public string scheduleId;

        [ObservableProperty]
        public int initialPaymentMethod;

        [ObservableProperty]
        public string passengerName;

        [ObservableProperty]
        public string gender;

        [RelayCommand]
        private async Task BookTicketAsync()
        {
            
        }

        [RelayCommand]
        private async Task ReserveTicketAsync()
        {
            
        }
    }
}
