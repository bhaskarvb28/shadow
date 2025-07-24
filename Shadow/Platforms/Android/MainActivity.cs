using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Shadow;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        var color = Android.Graphics.Color.ParseColor("#36393F");
        Window.SetStatusBarColor(color);
        Window.SetNavigationBarColor(color);

        Microsoft.Maui.ApplicationModel.Platform.Init(this, savedInstanceState);
    }
}
