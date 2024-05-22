using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HukutaiaExplorer.MVVM.Models;
using HukutaiaExplorer.MVVM.Services;
using HukutaiaExplorer.MVVM.Views;
using PropertyChanged;
using System;

namespace HukutaiaExplorer.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public partial class PlantsViewModel 
    {
        #region Fields
        //Private field to be used within ViewModel
        private List<Plant>? _plants;
        #endregion

        #region Propeties
        //Public property to be returned or viewed outside of ViewModel
        public List<Plant>? Plants { get; set; }

        // New property to store filtered plants
        public List<Plant>? FilteredPlants { get; set; }
        #endregion

        #region Constructor
        //In the constructor we call the LoadPlantsAsync method,
        //which calls the plant service (converting JSON into list objects),
        //then assigning the return value to the list of objects called "Plants".
        public PlantsViewModel() 
        {
            //No need to await here
            LoadPlantsAsync();
        }
        #endregion

        #region Methods
        // Navigates to plant details page
        [RelayCommand]
        public async Task GoToDetailsAsync(Plant plant)
        {
            // Check if the provided plant is null.
            if (plant == null) 
                return;

            // Create a new instance of PlantDetailsViewModel.
            var detailsViewModel = new PlantDetailsViewModel();
            // Set the Plant property of the PlantDetailsViewModel to the provided plant.
            detailsViewModel.Plant = plant;

            // Create a new instance of DetailsPage.
            var detailsPage = new DetailsPage();
            // Set the BindingContext of the DetailsPage to the PlantDetailsViewModel instance.
            detailsPage.BindingContext = detailsViewModel;

            // Navigate to the DetailsPage using traditional navigation.
            await Application.Current.MainPage.Navigation.PushAsync(detailsPage);
        }

        // Described above in constructor comments
        public async Task LoadPlantsAsync()
        {
            PlantService plantService = new PlantService();
            Plants = await plantService.GetPlants();

            // Initialize FilteredPlants with all plants initially
            FilteredPlants = Plants;
        }

        // Method to filter plants based on search query
        public void FilterPlants(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                // If search query is empty, show all plants
                FilteredPlants = Plants;
            }
            else
            {
                // Filter plants based on search query
                FilteredPlants = Plants?.Where(plant => 
                plant.Name?.ToLower().Contains(searchQuery.ToLower()) == true).ToList();
            }
        }
        #endregion
    }
}
