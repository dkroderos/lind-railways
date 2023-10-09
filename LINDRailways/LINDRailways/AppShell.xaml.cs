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
            Routing.RegisterRoute(nameof(AccountDetailsPage), typeof(AccountDetailsPage));
            Routing.RegisterRoute(nameof(AddAccountPage), typeof(AddAccountPage));
            Routing.RegisterRoute(nameof(AddTrainSchedulePage), typeof(AddTrainSchedulePage));
            Routing.RegisterRoute(nameof(TrainScheduleDetailsPage), typeof(TrainScheduleDetailsPage));
        }
    }
}