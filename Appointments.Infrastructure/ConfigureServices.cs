using Appointments.Application.Common.Interfaces.Persistence;
using Appointments.Infrastructure.Common.Interfaces;
using Appointments.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Appointments.Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IAppointmentRepository, AppointmentRepository>(); 
        services.AddSingleton<ISqlConnectionProvider, SqlConnectionProvider>();
    }
}