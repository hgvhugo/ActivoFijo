using ActivoFijo.Data;
using ActivoFijo.Dtos;
using ActivoFijo.Dtos.ActivoFijo.Dtos;
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
            registroBienes.Activo = true;
            registroBienes.FechaCreacion = DateTime.Now;
            registroBienes.EstatusId = 0;
            registroBienes.EstadoFisicoId = 2;
            await _context.RegistroBienes.AddAsync(registroBienes);
            await _context.SaveChangesAsync();
            return registroBienes;
        }

        public async Task<RegistroBienes> DeleteRegistroBienes(int id)
        {
            var registroBienes = await _context.RegistroBienes.FirstOrDefaultAsync(x => x.Id == id);
            if (registroBienes != null)
            {
                registroBienes.Activo = false;
                _context.RegistroBienes.Update(registroBienes);
                await _context.SaveChangesAsync();
            }
            return registroBienes;
        }

        public async Task<IEnumerable<RegistroBienes>> GetRegistroBienes()
        {
            var query = _context.RegistroBienes.AsQueryable();
            query = RepositoryUtils.IncludeNavigationProperties(_context, query);
            return await query.ToListAsync();
        }


        public async Task<RegistroBienes> GetRegistroBienes(int id)
        {
            var query = _context.RegistroBienes.AsQueryable();
            query = RepositoryUtils.IncludeNavigationProperties(_context, query);
            return await query.FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<IEnumerable<RegistroBienes>> GetRegistroBienesByUnidadAdministrativaId(int unidadAdministrativaId)
        {
            var query = _context.RegistroBienes.AsQueryable();
            query = RepositoryUtils.IncludeNavigationProperties(_context, query);
            query = query.Where(rb => rb.UnidadAdministrativaId == unidadAdministrativaId);
            return await query.ToListAsync();
        }


        public async Task<IEnumerable<RegistroBienes>> GetRegistroBienesByCriteria(int unidadAdministrativaId, int? estadoId = null, int? partidaId = null, int? estatusId = null, int? cambId = null, string? contrato = null, int? cucopId = null)
        {
            var query = _context.RegistroBienes.AsQueryable();
            query = RepositoryUtils.IncludeNavigationProperties(_context, query);

            query = query.Where(rb => rb.UnidadAdministrativaId == unidadAdministrativaId);

            query = query.Where(rb => rb.Activo == true);


            if (estadoId.HasValue)
            {
                query = query.Where(rb => rb.EstatusId == estadoId.Value);
            }

            if (partidaId.HasValue)
            {
                query = query.Where(rb => rb.PartidaId == partidaId.Value);
            }

            if (estatusId.HasValue)
            {
                query = query.Where(rb => rb.EstatusId == estatusId.Value);
            }

            if (cambId.HasValue)
            {
                query = query.Where(rb => rb.CambId == cambId.Value);
            }

            if (!string.IsNullOrEmpty(contrato))
            {
                query = query.Where(rb => rb.NumeroContrato == contrato);
            }

            if (cucopId.HasValue)
            {
                query = query.Where(rb => rb.CucopId == cucopId.Value);
            }

            return await query.ToListAsync();
        }


        public async Task<IEnumerable<ContadorBienesPorUnidadDto>> ObtenerConteosPorUnidadAsync()
        {
            var conteos = await _context.RegistroBienes
                .Include(rb => rb.UnidadAdministrativa) // Incluir la relación de navegación
                .GroupBy(rb => new { rb.UnidadAdministrativaId, rb.UnidadAdministrativa.Codigo }) // Agrupar por ID y nombre
                .Select(g => new ContadorBienesPorUnidadDto
                {
                    UnidadAdministrativaId = g.Key.UnidadAdministrativaId,
                    NombreUnidadAdministrativa = g.Key.Codigo,
                    Asignados = g.Count(rb => rb.EstatusId == 1 && rb.Activo == true),
                    NoAsignados = g.Count(rb => rb.EstatusId == 0 && rb.Activo == true),
                    Mantenimiento = g.Count(rb => rb.EstatusId == 2 && rb.Activo == true),
                    Baja = g.Count(rb => rb.Activo == false),
                })
                .ToListAsync();

            return conteos;
        }
    }
}
