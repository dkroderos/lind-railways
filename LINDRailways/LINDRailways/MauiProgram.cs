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
            builder.Services.AddSingleton<PendingTicketsViewModel>();
            builder.Services.AddSingleton<TicketsViewModel>();

            builder.Services.AddTransient<AddTicketViewModel>();
            builder.Services.AddTransient<TicketDetailsViewModel>();

            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<TrainScheduleOldsPage>();
            builder.Services.AddSingleton<PendingTicketsPage>();
            builder.Services.AddSingleton<TicketsPage>();

            builder.Services.AddTransient<AddTicketPage>();
            builder.Services.AddTransient<TicketDetailsPage>();

            return builder.Build();
        }
    }
}