using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PhotoBackupUtility.App.Configuration;

namespace PhotoBackupUtility.App;

public static class Program
{
	public static async Task Main(string[] args)
	{
		ServiceCollection serviceCollection = new ServiceCollection();
		RegisterServices(serviceCollection);

		ServiceProvider provider = serviceCollection.BuildServiceProvider();
		
		Application app = provider.GetService<Application>();
		
		await app.Call();
	}

	private static void RegisterServices(IServiceCollection services)
	{
		services.AddScoped<IAmazonS3, AmazonS3Client>();
		services.AddScoped<IBackupStateService, BackupStateService>();
		services.AddScoped<IFileCopyService, FileCopyService>();
		services.AddScoped<Application>();
		services.AddLogging(config => config.AddConsole());
		
		BuildConfiguration(services);
	}

	private static void BuildConfiguration(IServiceCollection services)
	{
		var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("configuration.json", false)
			.Build();

		services.Configure<ISettings>(config => configuration.GetSection("App").Bind(config));
	}
}
