using Appointments.Application;
using Appointments.Infrastructure;

namespace Appointments.Api;

public class Startup
{
    /// <summary>
    /// Creates a new startup instance
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// The Global configuration object
    /// </summary>
    private IConfiguration Configuration { get; }


    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        
        // Setup DDD-Structure
        services.AddApplication();
        services.AddInfrastructure(configuration);
        
    }
}