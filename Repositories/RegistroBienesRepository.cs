using ActivoFijo.Data;
using ActivoFijo.Models;
using ActivoFijo.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ActivoFijo.Repositories
{
    public class RegistroBienesRepository : IRegistroBienesRepository
    {
        private readonly AppDbContext _context;

        public RegistroBienesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RegistroBienes> CreateRegistroBienes(RegistroBienes registroBienes)
        {
            await _context.RegistroBienes.AddAsync(registroBienes);
            await _context.SaveChangesAsync();
            return registroBienes;
        }

        public async Task<RegistroBienes> DeleteRegistroBienes(int id)
        {
            var registroBienes = await _context.RegistroBienes.FirstOrDefaultAsync(x => x.Id == id);
            if (registroBienes != null)
            {
                _context.RegistroBienes.Remove(registroBienes);
                await _context.SaveChangesAsync();
            }
            return registroBienes;
        }

        public async Task<IEnumerable<RegistroBienes>> GetRegistroBienes()
        {
            return await _context.RegistroBienes.ToListAsync();
        }


        public async Task<RegistroBienes> GetRegistroBienes(int id)
        {
            return await _context.RegistroBienes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RegistroBienes> UpdateRegistroBienes(RegistroBienes registroBienes)
        {
            _context.RegistroBienes.Update(registroBienes);
            await _context.SaveChangesAsync();
            return registroBienes;
        }

        public async Task<RegistroBienes> GetRegistroBienesByCodigoBien(string codigo)
        {
            return await _context.RegistroBienes.FirstOrDefaultAsync(x => x.CodigoBien.Trim() == codigo.Trim() );
        }
    }
}
