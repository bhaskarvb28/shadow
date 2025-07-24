using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Android.Graphics.Drawables;
using CommunityToolkit.Maui;

namespace Shadow
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                 .UseMauiCommunityToolkit(); ;

#if DEBUG
    		builder.Logging.AddDebug();
#endif

#if ANDROID
    EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
    {
        if (handler.PlatformView is Android.Widget.EditText editText)
        {
            // Remove underline/background
            editText.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
        }
    });
#endif

            return builder.Build();
        }
    }
}
