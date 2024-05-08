using HukutaiaExplorer.MVVM.ViewModels;
using HukutaiaExplorer.MVVM.Models;
using System.Collections.ObjectModel;

namespace HukutaiaExplorer.MVVM.Views;

public partial class PlantsView : ContentPage
{
	public PlantsView()
	{
		InitializeComponent();
		PlantsViewModel viewModel = new PlantsViewModel();
		BindingContext = viewModel;
	}

    // override method for OnAppearing to load plants for populating Collection View data template.
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is PlantsViewModel viewModel)
        {
            await viewModel.LoadPlantsAsync();
        }
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Check if BindingContext is PlantsViewModel
        if (BindingContext is PlantsViewModel viewModel)
        {
            // Call FilterPlants method with the search query
            viewModel.FilterPlants(e.NewTextValue);
        }
    }

}