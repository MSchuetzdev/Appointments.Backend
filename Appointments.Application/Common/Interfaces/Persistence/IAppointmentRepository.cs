using Appointments.Application.Commands.Appointments;
using Appointments.Domain.Entities.Appointment;

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
    public Task<Appointment> CreateAsync(CreateAppointmentCommand command);

    /// <summary>
    /// Gets an appointment by id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<Appointment> ReadByIdAsync(Guid id);

    /// <summary>
    /// Updates an appointment
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public Task<Appointment> UpdateAsync(UpdateAppointmentCommand command);

    /// <summary>
    /// Cancel an single appointment by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<Appointment> CancelAppointmentByIdAsync(Guid id);
}