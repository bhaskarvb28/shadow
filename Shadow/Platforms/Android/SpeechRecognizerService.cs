#if ANDROID
using Android.Content;

namespace Shadow;

public static class SpeechRecognizerService
{
    public static TaskCompletionSource<string> SpeechTCS;

    public static Task<string> StartSpeechToTextAsync()
    {
        SpeechTCS = new TaskCompletionSource<string>();

        var context = Android.App.Application.Context;
        var intent = new Intent(context, typeof(SpeechActivity));
        intent.SetFlags(ActivityFlags.NewTask);
        context.StartActivity(intent);

        return SpeechTCS.Task;
    }
}
#endif
