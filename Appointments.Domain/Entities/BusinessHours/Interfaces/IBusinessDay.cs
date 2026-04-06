namespace Appointments.Domain.Entities.BusinessHours.Interfaces;

public interface IBusinessDay
{
    /// <summary>
    /// Id of the BusinessDay
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Day of the week
    /// </summary>
    public DayOfWeek WeekDay { get; set; }

    /// <summary>
    /// Start time of the day
    /// </summary>
    public TimeOnly? StartTime { get; set; }

    /// <summary>
    /// End time of the day
    /// </summary>
    public TimeOnly? EndTime { get; set; }

    /// <summary>
    /// Shows if the working time is over
    /// </summary>
    public bool IsClosed => StartTime >= EndTime || (EndTime == null && StartTime == null);
}