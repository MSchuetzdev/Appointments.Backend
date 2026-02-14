using Appointments.Application;
using Appointments.Infrastructure;

public class Program
{
    public static async Task Main(string[] args)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true);

        var configuration = configurationBuilder.Build();
    }


    public static IHost ConfigureServices(string[] args, IConfiguration configuration)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(builder =>
            {
                builder.Sources.Clear();
                builder.AddConfiguration(configuration);
            }).ConfigureServices(services =>
            {
                services.AddApplication();
                services.AddInfrastructure(configuration);
            }).Build();
    }
}