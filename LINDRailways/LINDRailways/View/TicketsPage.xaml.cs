using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class TicketsPage : ContentPage
{
    public TicketsPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        BindingContext = new TicketsViewModel();
    }

}