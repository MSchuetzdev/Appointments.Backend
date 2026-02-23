namespace Appointments.Infrastructure.Common.Models.Record;

public class CustomerRecord
{
    /// <summary>
    /// Id of the customer
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Firstname of the customer 
    /// </summary>
    public string FirstName { get; set; } = "";

    /// <summary>
    /// Lastname of the customer
    /// </summary>
    public string LastName { get; set; } = "";

    /// <summary>
    /// Id of the person 
    /// </summary>
    public Guid PersonId { get; set; }

    /// <summary>
    /// Id of the appointment
    /// </summary>
    public Guid AppointmentId { get; set; }
}