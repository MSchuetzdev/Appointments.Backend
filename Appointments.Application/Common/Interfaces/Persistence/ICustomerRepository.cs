using Appointments.Domain.Entities.Customer;

namespace Appointments.Application.Common.Interfaces.Persistence;

/// <summary>
/// Handles methods to interact with customers
/// </summary>
public interface ICustomerRepository
{
    /// <summary>
    /// Get an customer by its personId
    /// </summary>
    /// <param name="personId"></param>
    /// <returns></returns>
    public Task<Customer> GetCustomerByPersonIdAsync(Guid personId);
}