using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class AddTicketOldPage : ContentPage
{
	public AddTicketOldPage(AddTicketOldViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}