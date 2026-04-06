namespace Appointments.Domain.Entities.BusinessHours.Interfaces;

public interface IBusinessHours
{
    /// <summary>
    /// Business hours for monday
    /// </summary>
    public BusinessDay? Monday { get; set; }

    /// <summary>
    /// Business hours for tuesday
    /// </summary>
    public BusinessDay? Tuesday { get; set; }

    /// <summary>
    /// Business hours for wednesday
    /// </summary>
    public BusinessDay? Wednesday { get; set; }

    /// <summary>
    /// Business hours for wednesday
    /// </summary>
    public BusinessDay? Thursday { get; set; }

    /// <summary>
    /// Business hours for wednesday
    /// </summary>
    public BusinessDay? Friday { get; set; }

    /// <summary>
    /// Business hours for wednesday
    /// </summary>
    public BusinessDay? Saturday { get; set; }

    /// <summary>
    /// Business hours for wednesday
    /// </summary>
    public BusinessDay? Sunday { get; set; }
}