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

	//override method for OnAppearing to load plants for populating Collection View data template.
    protected override async void OnAppearing()
    {
        base.OnAppearing();
		if (BindingContext is PlantsViewModel viewModel)
		{
			await viewModel.LoadPlantsAsync();
		}
    }
}