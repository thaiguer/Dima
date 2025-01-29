﻿using MudBlazor.Utilities;
using MudBlazor;

namespace Dima.Web;

public static class Configuration
{
    public static bool IsDarkMode = true;

    public const string HttpClientName = "dima";

    //public static string BackendUrl { get; set; } = "https://localhost:5292";
    public static string BackendUrl { get; set; } = "https://localhost:5250";

    public static MudTheme Theme = new()
    {
        Typography = new Typography
        {
            Default = new Default
            {
                FontFamily = ["Mono", "Raleway", "sans-serif"]
            }
        },

        PaletteLight = new PaletteLight
        {
            Primary = new MudColor("#1EFA2D"),
            PrimaryContrastText = new MudColor("#000000"),
            Secondary = Colors.LightGreen.Darken3,
            Background = Colors.Gray.Lighten4,
            AppbarBackground = new MudColor("#1EFA2D"),
            AppbarText = Colors.Shades.Black,
            TextPrimary = Colors.Shades.Black,
            DrawerText = Colors.Shades.White,
            DrawerBackground = Colors.Green.Darken4
        },

        PaletteDark = new PaletteDark
        {
            Primary = Colors.LightGreen.Accent3,
            Secondary = Colors.LightGreen.Darken3,
            AppbarBackground = Colors.LightGreen.Accent3,
            AppbarText = Colors.Shades.Black,
            PrimaryContrastText = new MudColor("#000000")
        }
    };
}