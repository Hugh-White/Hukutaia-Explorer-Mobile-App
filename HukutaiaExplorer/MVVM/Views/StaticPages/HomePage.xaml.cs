
using HukutaiaExplorer.MVVM.ViewModels;

namespace HukutaiaExplorer.MVVM.Views.StaticPages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    #region Navigation Button Events
    private async void btnGuide_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new GuideView());
    }

    private async void btnAbout_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new AboutPage());

    }

    private async void btnWeb_Tapped(object sender, TappedEventArgs e)
    {
        await Launcher.OpenAsync(new Uri("https://www.bayconservation.nz/member/hukutaia-domain-care-group/"));
    }

    private async void btnPlants_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new PlantsView());
    }

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginView());
    }
    private async void btnCreatePlant_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UploadView());
    }
    
    #endregion

    
}