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
    /// Description of the appointment
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Starttime of the appointment
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Endtime of the appointment
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Id of the organization the appointment is created for 
    /// </summary>
    public Guid OrganizationId { get; set; }
    
    /// <summary>
    /// Id of the person who created the appointment
    /// </summary>
    public Guid PersonId { get; set; }

    /// <summary>
    /// Time when the appointment is cancelled
    /// </summary>
    public DateTime? DeletionTime { get; set; }

    /// <summary>
    /// Method to validate if appointment is cancelled
    /// </summary>
    /// <returns></returns>
    public bool IsAppointmentCancelled()
    {
        return DeletionTime != null;
    }
}