using ActivoFijo.Dtos.ActivoFijo.Dtos;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ActivoFijo.Repositories.IRepository
{
    public interface IRepositorioGenerico<T> where T : class
    {
        //Task<IEnumerable<T>> GetAllAsync(
        //    Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>> include = null);
        //Task<T> GetByIdAsync(int id);
        //Task AddAsync(T entity);
        //Task UpdateAsync(T entity);
        //Task DeleteAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        Task<IEnumerable<T>> GetAllByCriteriaAsync(Expression<Func<T, bool>> criteria);
 
    }
}
