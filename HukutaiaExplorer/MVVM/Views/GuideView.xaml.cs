namespace HukutaiaExplorer.MVVM.Views;
using HukutaiaExplorer.MVVM.ViewModels;

public partial class GuideView : ContentPage
{
    GuideViewModel vm = new GuideViewModel();
    public GuideView()
	{
		InitializeComponent();
		
		BindingContext = vm;
		vm.AddPinsToMap(hukMap);
		vm.DrawPolylinesOnMap(hukMap);
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await vm.StartLocationTrackingAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        vm.StopLocationTracking();
    }
}