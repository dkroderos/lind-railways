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
            builder.Services.AddSingleton<AccountDetailsViewModel>();
            builder.Services.AddSingleton<AddAccountViewModel>();

            builder.Services.AddTransient<AddTicketOldViewModel>();
            builder.Services.AddTransient<TicketOldDetailsViewModel>();

            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<TrainScheduleOldsPage>();
            builder.Services.AddSingleton<PendingTicketOldsPage>();
            builder.Services.AddSingleton<TicketOldsPage>();
            builder.Services.AddSingleton<AccountsPage>();
            builder.Services.AddSingleton<AccountDetailsPage>();
            builder.Services.AddSingleton<AddAccountPage>();

            builder.Services.AddTransient<AddTicketOldPage>();
            builder.Services.AddTransient<TicketOldDetailsPage>();

            return builder.Build();
        }
    }
}