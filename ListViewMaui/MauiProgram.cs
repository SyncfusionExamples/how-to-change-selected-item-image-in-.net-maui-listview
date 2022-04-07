using Syncfusion.Maui.ListView.Hosting;
namespace ListViewMaui;

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
				fonts.AddFont("Roboto-Medium.ttf", "RobotoMedium");
				fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
			});
		builder.ConfigureSyncfusionListView();
		return builder.Build();
	}
}
