using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class TicketsPage : ContentPage
{
	public TicketsPage(TicketsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}