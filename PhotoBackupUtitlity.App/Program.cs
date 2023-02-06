﻿using Amazon.S3;
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
		services.AddScoped<IBackupStateService, BackupStateService>();
		services.AddScoped<IFileCopyService, FileCopyService>();
		services.AddScoped<ICurrentBackupStateBuilder, CurrentBackupStateBuilder>();
		services.AddScoped<Application>();
		services.AddLogging(config => config.AddConsole());
		
		BuildConfiguration(services);
		BuildAwsClient(services);
	}

	private static void BuildAwsClient(IServiceCollection services)
	{
		services.AddScoped<IAmazonS3, AmazonS3Client>(provider =>
		{
			ISettings settings = provider.GetService<ISettings>();
			
			return new AmazonS3Client(settings.AwsKey, settings.AwsSecret);
		});
	}
	
	private static void BuildConfiguration(IServiceCollection services)
	{
		var configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("configuration.json", false)
			.Build();

		services.Configure<Settings>(config => configuration.GetSection("App").Bind(config));
		services.AddScoped<ISettings, Settings>();
	}
}
