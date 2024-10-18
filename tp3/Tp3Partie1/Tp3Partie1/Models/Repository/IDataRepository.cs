using Microsoft.AspNetCore.Mvc;

namespace Tp3Partie1.Models.Repository;

public interface IDataRepository<TEntity>
{
    ActionResult<IEnumerable<TEntity>> GetAll();
    ActionResult<TEntity> GetById(int id);
    // ActionResult<TEntity> GetByString(string str);
    Task<ActionResult<TEntity>> GetByStringAsync(string str);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
    Task DeleteAsync(TEntity entity);
}