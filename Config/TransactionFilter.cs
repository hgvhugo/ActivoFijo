using ActivoFijo.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ActivoFijo.Config
{

    public class TransactionFilter : IAsyncActionFilter
    {
        private readonly AppDbContext _dbContext;

        public TransactionFilter(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                var resultContext = await next();

                if (resultContext.Exception == null || resultContext.ExceptionHandled)
                {
                    await transaction.CommitAsync();
                }
                else
                {
                    await transaction.RollbackAsync();
                }
            }
        }
    }
}
