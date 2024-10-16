using ActivoFijo.Data;
using ActivoFijo.Dtos.ActivoFijo.Dtos;
using ActivoFijo.Models;
using ActivoFijo.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ActivoFijo.Repositories
{
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public RepositorioGenerico(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> query = _dbSet;
            query = RepositoryUtils.IncludeNavigationProperties(_context, query);
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            IQueryable<T> query = _dbSet;
            query = RepositoryUtils.IncludeNavigationProperties(_context, query);
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllByCriteriaAsync(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> query = _dbSet;
            query = RepositoryUtils.IncludeNavigationProperties(_context, query);
            return await query.Where(criteria).ToListAsync();
        }

        

        //private IQueryable<T> IncludeNavigationProperties(IQueryable<T> query)
        //{
        //    var entityType = _context.Model.FindEntityType(typeof(T));
        //    var navigationProperties = entityType.GetNavigations().Select(n => n.Name);

        //    foreach (var navigationProperty in navigationProperties)
        //    {
        //        query = query.Include(navigationProperty);
        //    }

        //    return query;
        //}


     
    }
}
