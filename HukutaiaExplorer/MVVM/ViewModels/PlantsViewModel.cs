using HukutaiaExplorer.MVVM.Models;
using HukutaiaExplorer.MVVM.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HukutaiaExplorer.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class PlantsViewModel
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
        //Described above in constructor comments
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
