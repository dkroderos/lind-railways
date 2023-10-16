using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LINDRailways.Model;
using LINDRailways.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.ViewModel
{
    [QueryProperty("TrainSchedule", "TrainSchedule")]
    public partial class TrainScheduleDetailsViewModel : BaseViewModel
    {
        public TrainScheduleDetailsViewModel()
        {
        }

        [ObservableProperty]
        private TrainSchedule trainSchedule;

        [RelayCommand]
        private async Task CancelTrainScheduleAsync()
        {
            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert("Cancel Schedule", "Are you sure you want to cancel this train schedule?",
                            "Yes", "No");

            if (!isConfirmed)
                return;

            bool isConfirmedTwiceReversed = await Shell.Current.CurrentPage.DisplayAlert(
                                        "DOUBLE CHECK", "This will refund all the tickets in this schedule", "Don't Continue", "Continue");

            if (isConfirmedTwiceReversed)
                return;

            try
            {
                var tickets = await TicketService.GetTicketsAsync();
                var trainTickets = from ticket in tickets
                                   where ticket.ScheduleId == TrainSchedule.Id
                                   select ticket;

                //DateTime departureDateTime = DateTime.Parse(TrainSchedule.DepartureDate).Add(DateTime.Parse(TrainSchedule.DepartureTime).TimeOfDay);

                foreach (Ticket trainTicket in trainTickets)
                {
                    if (trainTicket.IsPaid == 1)
                    {
                        var ticketAccount = await AccountService.GetAccountAsync(trainTicket.AccountUsername);

                        ticketAccount.Balance += (int)(trainTicket.IsBook == 1 ? TrainSchedule.Price * 0.8 : TrainSchedule.Price);
                    } 

                    await TicketService.RemoveTicketAsync(trainTicket.Id);
                }

                await TrainScheduleService.RemoveTrainScheduleAsync(TrainSchedule.Id);

                await Shell.Current.CurrentPage.DisplayAlert("Success!",
                    $"Train schedule removed", "OK");

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                await Shell.Current.CurrentPage.DisplayAlert("Error!",
                    $"Unable to delete train schedule: {ex.Message}", "OK");
            }
        }
    }
}
