using RailwayReservationSystem.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayReservationSystem.MVVM.ViewModel
{
    class BookingViewModel : ObservableObject
    {
        public RelayCommand OneWayBookingViewCommand { get; set; }
        public RelayCommand RoundTripBookingViewCommand { get; set; }

        public OneWayBookingViewModel OneWayBookingVM { get; set; }
        public RoundTripBookingViewModel RoundTripBookingVM { get; set; }

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

        public BookingViewModel()
        {
            OneWayBookingVM = new OneWayBookingViewModel();
            RoundTripBookingVM = new RoundTripBookingViewModel();

            OneWayBookingViewCommand = new RelayCommand(o =>
            {
                CurrentView = OneWayBookingVM;
            });
            RoundTripBookingViewCommand = new RelayCommand(o =>
            {
                CurrentView = RoundTripBookingVM;
            });
        }
    }
}
