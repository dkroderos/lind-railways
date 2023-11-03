using LINDRailways.View;

namespace LINDRailways
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AccountDetailsPage), typeof(AccountDetailsPage));
            Routing.RegisterRoute(nameof(AddAccountPage), typeof(AddAccountPage));
            Routing.RegisterRoute(nameof(AddTrainSchedulePage), typeof(AddTrainSchedulePage));
            Routing.RegisterRoute(nameof(TrainScheduleDetailsPage), typeof(TrainScheduleDetailsPage));
            Routing.RegisterRoute(nameof(AddTicketPage), typeof(AddTicketPage));
            Routing.RegisterRoute(nameof(TicketDetailsPage), typeof(TicketDetailsPage));
        }
    }
}