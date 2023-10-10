using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class AddTicketPage : ContentPage
{
	public AddTicketPage(AddTicketViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}