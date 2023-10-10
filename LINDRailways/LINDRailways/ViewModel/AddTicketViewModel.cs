using CommunityToolkit.Mvvm.ComponentModel;
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
        public string accountId;

        [ObservableProperty]
        public int scheduleId;

        [ObservableProperty]
        public string isPaid;

        [ObservableProperty]
        public int initialPaymentMethod;

        [ObservableProperty]
        public string passengerName;

        [ObservableProperty]
        public string gender;
    }
}
