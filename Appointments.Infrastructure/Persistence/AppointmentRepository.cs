using Appointments.Application.Common.Interfaces.Repositories;
using Appointments.Domain.Entities.Appointment;

namespace Appointments.Infrastructure.Persistence;

public class AppointmentRepository: IAppointmentRepository
{
    public Task<Appointment> CreateAsync(Appointment entity )
    {
        throw new NotImplementedException();
    }

    public Task<Appointment> UpdateAsync(Appointment entity)
    {
        throw new NotImplementedException();
    }

    public Task<Appointment> DeleteAsync(Appointment entity)
    {
        throw new NotImplementedException();
    }

    public Task<Appointment> CreateAsync(int test)
    {
        throw new NotImplementedException();
    }
}