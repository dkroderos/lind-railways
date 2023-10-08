using LINDRailways.View;

namespace LINDRailways
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AddTicketOldPage), typeof(AddTicketOldPage));
            Routing.RegisterRoute(nameof(TicketOldDetailsPage), typeof(TicketOldDetailsPage));
            Routing.RegisterRoute(nameof(TrainScheduleOldsPage), typeof(TrainScheduleOldsPage));
        }
    }
}