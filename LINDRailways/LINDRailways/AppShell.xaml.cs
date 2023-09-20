using LINDRailways.View;

namespace LINDRailways
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AddTicketPage), typeof(AddTicketPage));
            Routing.RegisterRoute(nameof(TicketDetailsPage), typeof(TicketDetailsPage));
            Routing.RegisterRoute(nameof(TrainSchedulesPage), typeof(TrainSchedulesPage));
        }
    }
}