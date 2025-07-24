#if ANDROID
using Android.App;
using Android.Content;
using Android.OS;
using Android.Speech;

namespace Shadow;

[Activity(NoHistory = true)]
public class SpeechActivity : Activity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        var intent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
        intent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
        intent.PutExtra(RecognizerIntent.ExtraPrompt, "Say something...");
        intent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

        StartActivityForResult(intent, 1001);
    }

    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
    {
        base.OnActivityResult(requestCode, resultCode, data);

        if (requestCode == 1001 && SpeechRecognizerService.SpeechTCS != null)
        {
            if (resultCode == Result.Ok && data != null)
            {
                var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                SpeechRecognizerService.SpeechTCS.TrySetResult(matches?[0] ?? string.Empty);
            }
            else
            {
                SpeechRecognizerService.SpeechTCS.TrySetException(new Exception("Speech canceled or failed."));
            }
        }

        Finish(); // Close this activity
    }
}
#endif
