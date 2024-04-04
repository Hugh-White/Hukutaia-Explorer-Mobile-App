using Android.App;
using Android.Content.PM;
using Android.OS;

namespace HukutaiaExplorer
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Setting bottom navbar to black
            Window.SetNavigationBarColor(Android.Graphics.Color.Black);

            //Setting orientation to lock in portrait mode
            RequestedOrientation = ScreenOrientation.Portrait;

            base.OnCreate(savedInstanceState);

            
        }
    }
}
