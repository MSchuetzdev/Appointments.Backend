using Appointments.Application.Commands.Appointments;
using Appointments.Domain.Entities.Appointment;

namespace Appointments.Application.Common.Interfaces.Persistence;

/// <summary>
/// Interface provides methods for handles booked appointments
/// </summary>
public interface IBookedAppointmentRepository
{
    /// <summary>
    /// Get booked appointments by appointmentId 
    /// </summary>
    /// <param name="appointmentId"></param>
    /// <returns></returns>
    public Task<BookedAppointment> ReadByIdAsync(Guid appointmentId);

    /// <summary>
    /// Book an appointment
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public Task<BookedAppointment> BookAppointmentAsync(BookAppointmentCommand command);

    /// <summary>
    /// Get booked appointment by appointment id
    /// </summary>
    /// <param name="appointmentId"></param>
    /// <returns></returns>
    public Task<BookedAppointment> GetBookedAppointmentByAppointmentIdAsync(Guid appointmentId);
}