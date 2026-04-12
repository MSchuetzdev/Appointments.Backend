using Appointments.Domain.Common;

namespace Appointments.Domain.Entities.Customer;

/// <summary>
/// Represents a customer
/// </summary>
public class Customer
{
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
}