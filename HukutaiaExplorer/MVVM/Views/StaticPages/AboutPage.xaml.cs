namespace HukutaiaExplorer.MVVM.Views.StaticPages;

using System.Collections;
using System.Timers;


public partial class AboutPage : ContentPage
{
    Timer timer;

    
	public AboutPage()
	{
		InitializeComponent();
        #region CarouselView Timer
        timer = new Timer(4000); // 4000 milliseconds = 4 seconds
        timer.AutoReset = true;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
        #endregion
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