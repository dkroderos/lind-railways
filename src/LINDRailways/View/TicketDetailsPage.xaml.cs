using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class TicketDetailsPage : ContentPage
{
	public TicketDetailsPage(TicketDetailsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
	protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}