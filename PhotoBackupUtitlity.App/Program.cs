using Amazon.S3;
using Microsoft.Extensions.DependencyInjection;

namespace PhotoBackupUtility.App;

public static class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("Backing Up Your Photos");

		ServiceCollection serviceCollection = new ServiceCollection();
		RegisterServices(serviceCollection);

		ServiceProvider provider = serviceCollection.BuildServiceProvider();
		
		Application app = provider.GetService<Application>();
		
		app.Call();
	}

	private static IServiceCollection RegisterServices(IServiceCollection services)
	{
		services.AddScoped<IAmazonS3>(provider => new AmazonS3Client());
		services.AddScoped<IBackupStateService, BackupStateService>();
		services.AddScoped<IFileCopyService, FileCopyService>();
		services.AddScoped<Application>();
		return services;
	}
}
