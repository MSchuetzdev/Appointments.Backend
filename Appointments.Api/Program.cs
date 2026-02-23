using Dapper;

namespace Appointments.Api;

public class Program
{
    public static Task Main(string[] args)
    {
        // Instruct dapper to map fields with underscore to PascalCase: first_name => FirstName
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        return CreateHostBuilder(args).Build().RunAsync();
    }


    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
    }
}