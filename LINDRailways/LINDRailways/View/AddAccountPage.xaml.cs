using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class AddAccountPage : ContentPage
{
	public AddAccountPage(AddAccountViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}