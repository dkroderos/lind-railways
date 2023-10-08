using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class AddAccountPage : ContentPage
{
    public AddAccountPage(AddAccountViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}