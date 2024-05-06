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

        #region LoadPlantsAsync Task
        //Described above in constructor comments
        public async Task LoadPlantsAsync()
        {
            PlantService plantService = new PlantService();
            Plants = await plantService.GetPlants();
        }
        #endregion
    }
}
