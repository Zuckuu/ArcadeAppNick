using Microsoft.Extensions.Logging;

namespace ArcadeAppNick;
using CommunityToolkit.Maui;
using ArcadeAppNick.Models;
using Microsoft.Maui.Storage;


public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkitMediaElement()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

		string dbpath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "users.db3");
        builder.Services.AddSingleton<UserRepository>(
			s => ActivatorUtilities.CreateInstance<UserRepository>(s, dbpath)
		);
		//builder.Services.AddTransient<Login>(); 


#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
