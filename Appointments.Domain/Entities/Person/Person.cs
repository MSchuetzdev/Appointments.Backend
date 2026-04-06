namespace Appointments.Domain.Entities.Person;

/// <summary>
/// Represents a person 
/// </summary>
public class Person
{
    /// <summary>
    /// Id of the person
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Firstname of the person
    /// </summary>
    public string Firstname { get; set; } = "";

    /// <summary>
    /// Lastname of the person
    /// </summary>
    public string Lastname { get; set; } = "";
    
    /// <summary>
    /// Email of the person
    /// </summary>
    public string Email { get; set; } = "";
    
    /// <summary>
    /// Phonenumber of the person
    /// </summary>
    public string PhoneNumber { get; set; } = "";
}