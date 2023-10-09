using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class TicketOldsPage : ContentPage
{
	public TicketOldsPage(TicketOldsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}