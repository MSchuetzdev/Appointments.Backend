using Appointments.Domain.Entities.BusinessHours.Interfaces;

namespace Appointments.Domain.Entities.BusinessHours;

public class BusinessDay : IBusinessDay
{
    /// <inheritdoc/>>
    public Guid Id { get; set; }

    /// <inheritdoc/>>
    public DayOfWeek WeekDay { get; set; }

    /// <inheritdoc/>>
    public TimeOnly? StartTime { get; set; }

    /// <inheritdoc/>>
    public TimeOnly? EndTime { get; set; }
}