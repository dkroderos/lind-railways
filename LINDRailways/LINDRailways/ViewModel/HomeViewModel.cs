using CommunityToolkit.Mvvm.ComponentModel;
using LINDRailways.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.ViewModel
{
    public partial class HomeViewModel : BaseViewModel
    {
        [ObservableProperty]
        private int balance = Account.Balance; 

        public HomeViewModel()
        {
            Title = "LIND Railways";
        }
    }
}
