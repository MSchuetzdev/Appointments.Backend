using Appointments.Domain.Common;
using Appointments.Domain.Entities.Appointment.Interfaces;

namespace Appointments.Domain.Entities.Appointment;

public class Appointment : BaseEntity<Guid>, IAppointment
{
    /// <inheritdoc/>>
    public string Name { get; set; } = "";

    /// <inheritdoc/>>
    public DateTime StartTime { get; set; }

    /// <inheritdoc/>>
    public DateTime EndTime { get; set; }

    /// <inheritdoc/>>
    public Customer.Customer Customer { get; set; } = new Customer.Customer();

    /// <inheritdoc/>>
    public DateTime? DeletionTime { get; set; }

    /// <summary>
    /// Method to check if appointment is cancelled
    /// </summary>
    /// <returns></returns>
    public bool IsAppointmentCancelled()
    {
        return DeletionTime != null;
    }
}