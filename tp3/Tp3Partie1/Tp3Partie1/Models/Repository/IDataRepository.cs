using Microsoft.AspNetCore.Mvc;

namespace Tp3Partie1.Models.Repository;

public interface IDataRepository<TEntity>
{
    Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync();
    Task<ActionResult<TEntity>> GetByIdAsync(int id);
    // ActionResult<TEntity> GetByString(string str);
    Task<ActionResult<TEntity>> GetByStringAsync(string str);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
    Task DeleteAsync(TEntity entity);
}