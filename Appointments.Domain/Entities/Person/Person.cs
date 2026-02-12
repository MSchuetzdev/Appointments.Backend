namespace Appointments.Domain.Entities.Person;

/// <summary>
/// Represents a person 
/// </summary>
public class Person
{
    /// <summary>
    /// Id of the person
    /// </summary>
    public required Guid PersonId { get; init; }

    /// <summary>
    /// Firstname of the person
    /// </summary>
    public string Firstname { get; set; } = "";

    /// <summary>
    /// Lastname of the person
    /// </summary>
    public string Lastname { get; set; } = "";
}