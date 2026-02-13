using Appointments.Application.Common.Interfaces.Repositories;
using Appointments.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Appointments.Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IAppointmentRepository, AppointmentRepository>(); 
    }
}