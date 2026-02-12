namespace Appointments.Domain.Entities.Customer;

/// <summary>
/// Represents a customer
/// </summary>
public class Customer
{
    /// <summary>
    /// Id of the customer
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Firstname of the customer 
    /// </summary>
    public string Firstname { get; set; } = "";

    /// <summary>
    /// Lastname of the customer
    /// </summary>
    public string Lastname { get; set; } = "";

    /// <summary>
    /// Id of the person 
    /// </summary>
    public Guid PersonId { get; init; }

    /// <summary>
    /// Id of the appointment
    /// </summary>
    public Guid AppointmentId { get; init; }
}