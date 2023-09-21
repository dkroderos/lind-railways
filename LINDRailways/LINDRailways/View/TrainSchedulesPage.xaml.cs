using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class TrainSchedulesPage : ContentPage
{
	public TrainSchedulesPage(TrainSchedulesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}