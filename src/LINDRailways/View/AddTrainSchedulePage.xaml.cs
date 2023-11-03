using LINDRailways.ViewModel;

namespace LINDRailways.View;

public partial class AddTrainSchedulePage : ContentPage
{
	public AddTrainSchedulePage()
	{
		InitializeComponent();
		BindingContext = new AddTrainScheduleViewModel();
	}
}