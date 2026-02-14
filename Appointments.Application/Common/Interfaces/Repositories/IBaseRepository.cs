namespace Appointments.Application.Common.Interfaces.Repositories;

public interface IBaseRepository<TEntity>
{
   Task<TEntity> CreateAsync(TEntity entity); 
   
   Task<TEntity> UpdateAsync(TEntity entity);
   
   Task<TEntity> DeleteAsync(TEntity entity);
   
}