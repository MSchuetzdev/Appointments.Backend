using Appointments.Domain.Entities.BusinessHours.Interfaces;

namespace Appointments.Domain.Entities.BusinessHours;

public class BusinessHours : IBusinessHours
{
    /// <inheritdoc/>>
    public BusinessDay? Monday { get; set; }

    /// <inheritdoc/>>
    public BusinessDay? Tuesday { get; set; }

    /// <inheritdoc/>>
    public BusinessDay? Wednesday { get; set; }

    /// <inheritdoc/>>
    public BusinessDay? Thursday { get; set; }

    /// <inheritdoc/>>
    public BusinessDay? Friday { get; set; }

    /// <inheritdoc/>>
    public BusinessDay? Saturday { get; set; }

    /// <inheritdoc/>>
    public BusinessDay? Sunday { get; set; }
}