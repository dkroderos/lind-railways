using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class AccountsPage : ContentPage
{
    public AccountsPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        BindingContext = new AccountsViewModel();
    }
}