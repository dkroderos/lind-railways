using LINDRailways.Model;
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
        }
    }
}
