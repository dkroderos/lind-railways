using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RailwayReservationSystem.MVVM.View
{
    /// <summary>
    /// Interaction logic for RoundTripBookingView.xaml
    /// </summary>
    public partial class RoundTripBookingView : UserControl
    {
        public RoundTripBookingView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Nakapagbook kana, Tulog ka muna, sleepwell!");
        }
    }
}
