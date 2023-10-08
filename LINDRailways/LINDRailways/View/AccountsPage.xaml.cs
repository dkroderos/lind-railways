using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class AccountsPage : ContentPage
{
	public AccountsPage(AccountsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}