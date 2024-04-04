namespace HukutaiaExplorer.MVVM.Views.StaticPages;

public partial class LandingPage : ContentPage
{
	public LandingPage()
	{
		InitializeComponent();
	}

    private async void btn_Start_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomePage());
    }
}