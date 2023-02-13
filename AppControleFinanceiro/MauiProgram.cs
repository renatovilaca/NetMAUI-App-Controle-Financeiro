using AppControleFinanceiro.Backend;
using AppControleFinanceiro.Backend.Repositories;
using AppControleFinanceiro.Backend.Repositories.Interfaces;
using LiteDB;
using Microsoft.Extensions.Logging;

namespace AppControleFinanceiro;

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
			.RegisterDatabaseAndRepositories();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

	public static MauiAppBuilder RegisterDatabaseAndRepositories(this MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<LiteDatabase>(
			options =>
			{
				return new LiteDatabase($"Filename={AppSettings.DatabasePath};Connection=Shared");
			}
		);
		builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();

		return builder;
    }
}
