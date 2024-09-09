using Microsoft.Extensions.Logging;
using Microsoft.Maui.Platform;

namespace TestToolbarItem;

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
            });

#if WINDOWS
        Microsoft.Maui.Handlers.ToolbarHandler.Mapper.AppendToMapping("MyCustomizationTB", (handler, view) =>
        {
            // FIX x Secondary Menu size on Windows
            handler.PlatformView.FontSize = 14;
            handler.PlatformView.SizeChanged += (f, o) =>
            {
                var toolbar = f as Microsoft.Maui.Platform.MauiToolbar;
                //toolbar.Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(50, 230, 230, 250));

                var grid = toolbar?.Content as Microsoft.UI.Xaml.Controls.Grid;
                if (grid != null)
                {                    
                    dynamic k = grid.Children[^2];
                    k.Child.FontSize = 28;
                }
            };

            // FIX x Secondary Menu items color on Windows
            handler.PlatformView.GotFocus += (f, o) =>
            {
                var toolbar = f as Microsoft.Maui.Platform.MauiToolbar;
                var commandBarInfo = toolbar!.GetType().GetProperty("CommandBar", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var commandbar = commandBarInfo.GetValue(toolbar);
                foreach (Microsoft.UI.Xaml.Controls.AppBarButton button in (commandbar as Microsoft.UI.Xaml.Controls.CommandBar).SecondaryCommands.Cast<Microsoft.UI.Xaml.Controls.AppBarButton>())
                {
                    button.UpdateTextColor(Color.FromArgb("#512BD4"));
                    button.FontSize = 14;
                }
            };
        });
#endif

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
