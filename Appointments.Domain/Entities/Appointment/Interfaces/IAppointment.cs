namespace Appointments.Domain.Entities.Appointment.Interfaces;

public interface IAppointment
{
    /// <summary>
    /// Id of the Appointment
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the appointment
    /// </summary>
    public string Name { get; set; }

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
    public Customer.Customer Customer { get; set; }
    
    /// <summary>
    /// Time when the appointment is cancelled
    /// </summary>
    public DateTime? DeletionTime { get; set; }
}