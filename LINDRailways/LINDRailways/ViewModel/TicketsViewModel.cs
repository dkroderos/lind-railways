using CommunityToolkit.Mvvm.Input;
using LINDRailways.Model;
using LINDRailways.Services;
using LINDRailways.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.ViewModel
{
    public partial class TicketsViewModel : BaseViewModel
    {
        public ObservableCollection<Ticket> Tickets { get; } = new();
        public TicketsViewModel()
        {
            Title = "Tickets";

            GetTickets();
        }

        [RelayCommand]
        private async Task GoToTicketDetailsAsync(Ticket ticket)
        {
            if (ticket is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(TicketDetailsPage)}",
                true, new Dictionary<string, object>
                {
                    { "Ticket", ticket }
                });
        }

        public async void GetTickets()
        {
            Tickets.Clear();

            TransportationEntity mugenTrain = new("Mugen Train",
                ".", "mugen_train.jpg", ".");

            TransportationEntity philippines = new("Philippines",
                ".", ".", ".");
            TransportationEntity japan = new("Japan", ".", ".", ".");

            TrainSchedule trainSchedule = new(mugenTrain,
                philippines, japan, new TimeOnly(18, 0));

            IEnumerable<Ticket> tickets = await TicketService.GetPaidTickets();
            foreach (Ticket ticket in tickets)
            {
                Tickets.Add(ticket);
            }
        }
    }
}
