using Appointments.Application;
using Appointments.Domain.Options;
using Appointments.Infrastructure;
using Microsoft.OpenApi;

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


    public void ConfigureServices(IServiceCollection services)
    {
        // Setup DDD-Structure
        services.AddApplication();
        services.AddInfrastructure(Configuration);

        // Add options
        services.Configure<ApiUrls>(Configuration.GetSection("ApiUrls"));

        // Add controllers
        services.AddControllers();

        // Configure Cors settings
        services.AddCors(x =>
        {
            x.AddPolicy("AllowAll", y =>
            {
                y.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .SetPreflightMaxAge(TimeSpan.FromHours(24));
            });
        });


        var medaitrLicenseKey = Environment.GetEnvironmentVariable("MEDIATR_LICENSE_KEY");


        services.AddMediator(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<Startup>(); 
        });
        
        // Configure Swagger settings
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Appointments.Api",
                Version = "v1"
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
        }

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.UseCors("AllowAll");
    }
}