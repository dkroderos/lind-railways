using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class PendingTicketsPage : ContentPage
{
	public PendingTicketsPage(PendingTicketsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}