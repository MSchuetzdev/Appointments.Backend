namespace Appointments.Domain.Entities.Organization;

/// <summary>
/// Represents an organization
/// </summary>
public class Organization
{
    /// <summary>
    /// Id of the organization
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the organization
    /// </summary>
    public string Name { get; set; } = "";
}