namespace Appointments.Domain.Entities.Employee;

/// <summary>
/// Represents an Employee 
/// </summary>
public class Employee
{
    /// <summary>
    /// Id of the employee
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Firstname of the employee
    /// </summary>
    public string FirstName { get; set; } = "";

    /// <summary>
    /// Lastname of the employee
    /// </summary>
    public string LastName { get; set; } = "";

    /// <summary>
    /// Id of the person where the employee belongs to 
    /// </summary>
    public Guid PersonId { get; set; }
}