using Appointments.Domain.Common;
using Appointments.Domain.Entities.Appointment.Interfaces;

namespace Appointments.Domain.Entities.Appointment;

/// <summary>
/// Model of an booked appointment
/// </summary>
public class AppointmentBooking : BaseEntity<Guid>, IAppointment
{
    /// <inheritdoc/>
    public string Name { get; set; } = "";

    /// <inheritdoc/>>
    public string? Description { get; set; }

    /// <inheritdoc/>
    public DateTime StartTime { get; set; }

    /// <inheritdoc/>
    public DateTime EndTime { get; set; }

    /// <inheritdoc/>
    public Guid OrganizationId { get; set; }

    /// <inheritdoc/>
    public Guid PersonId { get; set; }

    /// <inheritdoc/>
    public DateTime? DeletionTime { get; set; }

    /// <summary>
    /// Customer of the booked appointment
    /// </summary>
    public List<Customer.Customer> Customer { get; init; } = new List<Customer.Customer>();
}