namespace HukutaiaExplorer.MVVM.Views.StaticPages;
using System.Timers;
using System.Collections;

public partial class LandingPage : ContentPage
{
    Timer timer;

	public LandingPage()
	{
		InitializeComponent();
        #region CarouselView Timer
        timer = new Timer(4000); // 4000 milliseconds = 4 seconds
        timer.AutoReset = true;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
        #endregion
    }

    private async void btn_Start_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomePage());
    }

    [Obsolete]
    private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            if (slideShow.ItemsSource != null && slideShow.Position == ((IList)slideShow.ItemsSource).Count - 1)
                slideShow.Position = 0;
            else
                slideShow.Position++;
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        timer?.Stop();
        timer?.Dispose();
    }
}