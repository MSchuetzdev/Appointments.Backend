namespace Appointments.Infrastructure.Common.Models.Record;

/// <summary>
/// Record for getting appointment bookings
/// </summary>
public class AppointmentBookingRecord
{
    /// <summary>
    /// Id of the appointmentBooking
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// PersonId of the customer who booked the appointment
    /// </summary>
    public Guid CustomerPersonId { get; set; }

    /// <summary>
    /// Id of the appointment  
    /// </summary>
    public Guid AppointmentId { get; set; }

    /// <summary>
    /// Time of the booking
    /// </summary>
    public DateTime CreationTime { get; set; }
}