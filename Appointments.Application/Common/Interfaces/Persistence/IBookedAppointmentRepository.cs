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
    public Task<AppointmentBooking> ReadByIdAsync(Guid appointmentId);

    /// <summary>
    /// Book an appointment
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public Task<AppointmentBooking> BookAppointmentAsync(BookAppointmentCommand command);

    /// <summary>
    /// Get booked appointment by appointment id
    /// </summary>
    /// <param name="appointmentId"></param>
    /// <returns></returns>
    public Task<AppointmentBooking> GetBookedAppointmentByAppointmentIdAsync(Guid appointmentId);
    
    /// <summary>
    /// Cancel an booked appointment by its appointment id
    /// </summary>
    /// <param name="appointmentId"></param>
    /// <returns></returns>
    public Task CancelBookedAppointmentAsync(Guid appointmentId);
}