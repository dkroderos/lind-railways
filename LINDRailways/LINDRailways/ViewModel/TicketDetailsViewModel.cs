using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LINDRailways.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.ViewModel
{
    [QueryProperty("Ticket", "Ticket")]
    public partial class TicketDetailsViewModel : BaseViewModel
    {
        public TicketDetailsViewModel()
        {
        }

        [ObservableProperty]
        private Ticket ticket;

        [RelayCommand]
        private async Task CancelTicketAsync()
        {

        }

        [RelayCommand]
        private async Task PayTicketAsync()
        {

        }
    }
}
