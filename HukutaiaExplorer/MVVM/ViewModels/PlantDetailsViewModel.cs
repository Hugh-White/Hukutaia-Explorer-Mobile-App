using CommunityToolkit.Mvvm.ComponentModel;
using HukutaiaExplorer.MVVM.Models;
using PropertyChanged;

namespace HukutaiaExplorer.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    [QueryProperty("Plant", "Plant")]
    public class PlantDetailsViewModel : ObservableObject
    {
        private Plant plant;

        public PlantDetailsViewModel() 
        {
        }

        // Property representing the plant whose details are to be displayed.
        public Plant Plant
        {
            get => plant;
            // Use SetProperty to set the property value and raise PropertyChanged event.
            set => SetProperty(ref plant, value);
        }
    }
}
