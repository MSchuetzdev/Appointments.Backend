using Appointments.Database.Migration;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = CreateServices();

        // Put the database update into a scope to ensure
        // that all resources will be disposed.
        using (var scope = serviceProvider.CreateScope())
        {
            UpdateDatabase(scope.ServiceProvider);
        }
    }

    /// <summary>
    /// Configure the dependency injection services
    /// </summary>
    private static ServiceProvider CreateServices()
    {
        return new ServiceCollection()
            // Add common FluentMigrator services
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                // Add SQL Server support to FluentMigrator
                .AddPostgres()
                // Set the connection string
                .WithGlobalConnectionString(
                    "Host=localhost; Port=5432; Database=postgres; Username=user; Password=test")
                // Define the assembly containing the migrations
                .ScanIn(typeof(M001InitialMigration).Assembly).For.Migrations())
            // Enable logging to console in the FluentMigrator way
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            // Build the service provider
            .BuildServiceProvider(false);
    }

    /// <summary>
    /// Update the database
    /// </summary>
    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        // Instantiate the runner
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        // Execute the migrations
        runner.MigrateUp();
    }
}