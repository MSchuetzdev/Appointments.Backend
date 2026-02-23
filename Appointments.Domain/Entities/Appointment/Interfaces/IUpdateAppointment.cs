namespace Appointments.Domain.Entities.Appointment.Interfaces;

public interface IUpdateAppointment
{
    /// <summary>
    /// Id of the appointment, wich will be updated
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Updated name of the appointment
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Updated start time of the appointment
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Updated end time of the appointment
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Updated customer person id of the appointment
    /// </summary>
    public Guid CustomerPersonId { get; set; }
}