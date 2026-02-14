namespace Appointments.Application.Common.Interfaces.Repositories;

public interface IReadRepository<T>
{
    Task<IEnumerable<T>> GetAsync(string sqlFilter, object param); 
}