using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class PendingTicketOldsPage : ContentPage
{
	public PendingTicketOldsPage(PendingTicketOldsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}