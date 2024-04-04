using HukutaiaExplorer.MVVM.ViewModels;

namespace HukutaiaExplorer.MVVM.Views;

public partial class PlantsView : ContentPage
{
	public PlantsView()
	{
		InitializeComponent();
		PlantsViewModel viewModel = new PlantsViewModel();
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		if (BindingContext is PlantsViewModel viewModel)
		{
			await viewModel.LoadPlantsAsync();
		}
    }
}