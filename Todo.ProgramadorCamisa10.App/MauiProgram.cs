﻿using Microsoft.Extensions.Logging;

namespace Todo.ProgramadorCamisa10.App
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
                    fonts.AddFont("Oswald-Regular.ttf", "OswaldRegular");
                    fonts.AddFont("Oswald-Bold.ttf", "OswaldBold");
                    fonts.AddFont("Oswald-ExtraLight.ttf", "OswaldExtraLight");
                    fonts.AddFont("Oswald-Light.ttf", "OswaldLight");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}