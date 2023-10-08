using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class TrainSchedulesPage : ContentPage
{
	public TrainSchedulesPage()
	{
		InitializeComponent();
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        BindingContext = new TrainSchedulesViewModel();
    }
}