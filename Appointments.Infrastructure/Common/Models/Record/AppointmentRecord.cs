using Appointments.Domain.Common;

namespace Appointments.Infrastructure.Common.Models.Record;

public class AppointmentRecord : BaseEntity<Guid>
{
    /// <summary>
    /// Name of the appointment
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Start time of the appointment
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// end time of the appointment
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// id of the person where books the appointment
    /// </summary>
    public Guid PersonId { get; set; }

    /// <summary>
    /// Deletion time of the appointment
    /// </summary>
    public DateTime? DeletionTime { get; set; }
}