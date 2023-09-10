using RailwayReservationSystem.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayReservationSystem.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand TicketsViewCommand { get; set; }
        public RelayCommand ExpiredTicketsViewCommand { get; set; }
        public RelayCommand AddTicketViewCommand { get; set; }
        public RelayCommand MembersViewCommand { get; set; }
        public RelayCommand BookingViewCommand { get; set; }

        public TicketsViewModel TicketsVM { get; set; }
        public ExpiredTicketsViewModel ExpiredTicketsVM { get; set; }
        public AddTicketViewModel AddTicketVM { get; set; }
        public MembersViewModel MembersVM { get; set; }
        public BookingViewModel BookingVM { get; set; }

        private object currentView;

        public object CurrentView
        {
            get { return currentView; }
            set
            {
                currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            TicketsVM = new TicketsViewModel();
            ExpiredTicketsVM = new ExpiredTicketsViewModel();
            AddTicketVM = new AddTicketViewModel();
            MembersVM = new MembersViewModel();
            BookingVM = new BookingViewModel();

            CurrentView = BookingVM;

            TicketsViewCommand = new RelayCommand(o =>
            {
                CurrentView = TicketsVM;
            });
            ExpiredTicketsViewCommand = new RelayCommand(o =>
            {
                CurrentView = ExpiredTicketsVM;
            });
            AddTicketViewCommand = new RelayCommand(o =>
            {
                CurrentView = AddTicketVM;
            });
            MembersViewCommand = new RelayCommand(o =>
            {
                CurrentView = MembersVM;
            });
            BookingViewCommand = new RelayCommand(o =>
            {
                CurrentView = BookingVM;
            });
        }
    }
}
