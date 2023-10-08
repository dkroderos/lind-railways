using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class TrainScheduleOldsPage : ContentPage
{
	public TrainScheduleOldsPage(TrainScheduleOldsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}