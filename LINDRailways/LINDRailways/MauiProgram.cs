using LINDRailways.View;
using LINDRailways.ViewModel;
using Microsoft.Extensions.Logging;

namespace LINDRailways
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<TrainScheduleOldsViewModel>();
            builder.Services.AddSingleton<PendingTicketOldsViewModel>();
            builder.Services.AddSingleton<TicketOldsViewModel>();
            builder.Services.AddSingleton<AccountsViewModel>();
            builder.Services.AddSingleton<TrainSchedulesViewModel>();

            builder.Services.AddTransient<AddTicketOldViewModel>();
            builder.Services.AddTransient<TicketOldDetailsViewModel>();
            builder.Services.AddTransient<AccountDetailsViewModel>();
            builder.Services.AddTransient<AddAccountViewModel>();
            builder.Services.AddTransient<TrainScheduleDetailsViewModel>();

            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<TrainScheduleOldsPage>();
            builder.Services.AddSingleton<PendingTicketOldsPage>();
            builder.Services.AddSingleton<TicketOldsPage>();
            builder.Services.AddSingleton<AccountsPage>();
            builder.Services.AddSingleton<TrainSchedulesPage>();

            builder.Services.AddTransient<AddTicketOldPage>();
            builder.Services.AddTransient<TicketOldDetailsPage>();
            builder.Services.AddTransient<AccountDetailsPage>();
            builder.Services.AddTransient<AddAccountPage>();
            builder.Services.AddTransient<TrainScheduleDetailsPage>();

            return builder.Build();
        }
    }
}