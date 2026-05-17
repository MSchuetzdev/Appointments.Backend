using Appointments.Domain.Common;
using Appointments.Domain.Entities.Appointment.Interfaces;

namespace Appointments.Domain.Entities.Appointment;

public class Appointment : BaseEntity<Guid>, IAppointment
{
    /// <inheritdoc/>>
    public string Name { get; set; } = "";

    /// <inheritdoc/>>
    public string? Description { get; set; }

    /// <inheritdoc/>>
    public DateTime StartTime { get; set; }

    /// <inheritdoc/>
    public DateTime EndTime { get; set; }

    /// <inheritdoc/>
    public Guid OrganizationId { get; set; }

    /// <inheritdoc/>
    public Guid PersonId { get; set; }

    /// <inheritdoc/>
    public DateTime? DeletionTime { get; set; }
}