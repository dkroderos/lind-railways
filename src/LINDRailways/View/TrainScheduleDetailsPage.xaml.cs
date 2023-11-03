using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class TrainScheduleDetailsPage : ContentPage
{
	public TrainScheduleDetailsPage(TrainScheduleDetailsViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}