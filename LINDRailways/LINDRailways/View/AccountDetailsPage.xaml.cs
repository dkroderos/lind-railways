using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class AccountDetailsPage : ContentPage
{
	public AccountDetailsPage(AccountDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}