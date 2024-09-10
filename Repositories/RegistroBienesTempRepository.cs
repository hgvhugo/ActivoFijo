using ActivoFijo.Data;
using ActivoFijo.Models;
using ActivoFijo.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ActivoFijo.Repositories
{
    public class RegistroBienesTempRepository : IRegistroBienesTempRepository
    {
        private readonly AppDbContext _context;

        public RegistroBienesTempRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<RegistroBienesTemp> registros)
        {
            await _context.RegistroBienesTemp.AddRangeAsync(registros);
            await _context.SaveChangesAsync();
        }
    }
}
