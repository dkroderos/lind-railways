using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class TicketOldDetailsPage : ContentPage
{
	public TicketOldDetailsPage(TicketOldDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}