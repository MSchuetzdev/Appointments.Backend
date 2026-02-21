using Appointments.Application.Commands;
using Appointments.Domain.Entities.Appointment;

namespace Appointments.Application.Common.Interfaces.Repositories;

public interface IAppointmentRepository 
{
    /// <summary>
    /// Creates a new Appointment
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public Task<Appointment> CreateAsync(CreateAppointmentCommand command);
    
    public Task<Appointment> ReadByIdAsync(Guid id);
}