using Appointments.Application.Commands.Appointments;
using Appointments.Domain.Entities.Appointment.Interfaces;

namespace Appointments.Application.Common.Interfaces.Persistence;

/// <summary>
/// Interface provides methods for handles appointments in database context 
/// </summary>
public interface IAppointmentRepository
{
    /// <summary>
    /// Creates a new Appointment
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public Task<IAppointment> CreateAsync(CreateAppointmentCommand command);

    /// <summary>
    /// Gets an appointment by id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<IAppointment> ReadByIdAsync(Guid id);

    /// <summary>
    /// Updates an appointment
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public Task<IAppointment> UpdateAsync(UpdateAppointmentCommand command);

    /// <summary>
    /// Cancel an single appointment by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<IAppointment> CancelAppointmentByIdAsync(Guid id);
}