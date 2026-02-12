namespace Appointments.Domain.Entities.Appointment;

public class Appointment
{
    /// <summary>
    /// Id of the appointment
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Starttime of the appointment
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Endtime of the appointment
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Customer where booked the appointment
    /// </summary>
    public Customer.Customer Customer { get; set; } = new Customer.Customer();
}