using static Microsoft.Maui.ApplicationModel.Permissions;
using Microsoft.Maui.ApplicationModel; // Required for Permissions
using Microsoft.Maui.Media; // Required for TextToSpeech and Locale
using Microsoft.Maui.ApplicationModel.Communication; // Optional, depending on platform
using System.Linq;
using System.Threading.Tasks;

namespace Shadow;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnUrlEntered(object sender, EventArgs e)
    {
        var url = UrlEntry.Text?.Trim();

        if (!string.IsNullOrWhiteSpace(url))
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = "https://" + url;

            BrowserView.Source = url;
        }
    }

    private void OnHomeClicked(object sender, EventArgs e)
    {
        const string homeUrl = "https://www.bing.com";
        UrlEntry.Text = homeUrl;
        BrowserView.Source = homeUrl;
    }

    private void OnShareClicked(object sender, EventArgs e)
    {
        DisplayAlert("Share", "Share button clicked (functionality coming soon!)", "OK");
    }

    private async void OnMoreOptionsClicked(object sender, EventArgs e)
    {
#if ANDROID
        var permission = await Permissions.RequestAsync<Permissions.Microphone>();
        if (permission != PermissionStatus.Granted)
        {
            await DisplayAlert("Permission Denied", "Please grant microphone access.", "OK");
            return;
        }

        try
        {
            // Get available locales for TTS
            var locales = await TextToSpeech.GetLocalesAsync();
            var selectedLocale = locales.FirstOrDefault(l => l.Language == "en-US") ?? locales.FirstOrDefault(l => l.Language.StartsWith("en"));

            if (selectedLocale == null)
            {
                await DisplayAlert("Error", "No suitable TTS locale found.", "OK");
                return;
            }

            string spokenText = await SpeechRecognizerService.StartSpeechToTextAsync();
            if (!string.IsNullOrWhiteSpace(spokenText))
            {
                // Display the recognized text
                // await DisplayAlert("You said", spokenText, "OK");

                // Speak the recognized text back with consistent voice
                await TextToSpeech.SpeakAsync(spokenText, new SpeechOptions
                {
                    Locale = selectedLocale, // Use MAUI Locale instead of CultureInfo
                    Pitch = 1.0f, // Normal pitch (0.0 to 2.0)
                    Volume = 0.75f // Adjust volume (0.0 to 1.0)
                });

                // Respond to specific phrases
                if (spokenText.ToLower().Contains("hello"))
                {
                    //await DisplayAlert("Response", "Hi Bhaskar! How can I help you", "OK");
                    await TextToSpeech.SpeakAsync("Hi Bhaskar! How can I help you", new SpeechOptions
                    {
                        Locale = selectedLocale, // Same locale for consistency
                        Pitch = 1.0f,
                        Volume = 0.75f
                    });
                }
            }
            else
            {
                await DisplayAlert("Speech Failed", "No speech recognized.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Speech failed: {ex.Message}", "OK");
        }
#else
        await DisplayAlert("Error", "Speech recognition is only supported on Android in this implementation.", "OK");
#endif
    }

    // Optional: Method to list available voices for debugging
    private async Task ListAvailableVoices()
    {
        var locales = await TextToSpeech.GetLocalesAsync();
        var voices = locales.Select(l => $"{l.Name} ({l.Language})").ToList();
        await DisplayAlert("Available Voices", string.Join("\n", voices), "OK");
    }

    private void OnTabCountTapped(object sender, EventArgs e)
    {
        DisplayAlert("Tab", "More options clicked (menu coming soon!)", "OK");
    }
}