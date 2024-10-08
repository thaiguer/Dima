using MudBlazor.Utilities;
using MudBlazor;

namespace Dima.Web;

public static class Configuration
{
    public static bool IsDarkMode = false;

    public const string HttpClientName = "dima";

    public static string BackendUrl { get; set; } = "http://localhost:7119";

    public static MudTheme Theme = new()
    {
        Typography = new Typography
        {
            Default = new Default
            {
                FontFamily = ["Raleway", "sans-serif"]
            }
        },

        PaletteLight = new PaletteLight
        {
            Primary = new MudColor("#1EFA2D"),
            Secondary = Colors.LightGreen.Darken3,
            Background = Colors.Gray.Lighten4,
            AppbarBackground = new MudColor("1EFA2D"),
            AppbarText = Colors.Shades.Black,
            TextPrimary = Colors.Shades.Black,
            PrimaryContrastText = Colors.Shades.Black
        },

        PaletteDark = new PaletteDark
        {
            Primary = Colors.LightGreen.Accent3,
            Secondary = Colors.LightGreen.Darken3,
            Background = MudBlazor.Colors.Gray.Darken4,
            AppbarBackground = Colors.LightGreen.Accent3
        }
    };
}