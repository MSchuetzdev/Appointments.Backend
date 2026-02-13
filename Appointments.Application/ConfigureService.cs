using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Appointments.Application;

public static class ConfigureService
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

        }); 
    }
}