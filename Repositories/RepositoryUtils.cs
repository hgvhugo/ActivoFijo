using Microsoft.EntityFrameworkCore;

namespace ActivoFijo.Repositories
{
    public static class RepositoryUtils
    {
        public static IQueryable<T> IncludeNavigationProperties<T>(DbContext context, IQueryable<T> query) where T : class
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            var navigationProperties = entityType.GetNavigations().Select(n => n.Name);

            foreach (var navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            return query;
        }
    }
}
