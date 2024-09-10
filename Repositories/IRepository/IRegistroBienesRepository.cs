using ActivoFijo.Dtos;
using ActivoFijo.Dtos.ActivoFijo.Dtos;
using ActivoFijo.Models;

namespace ActivoFijo.Repositories.IRepository
{
    public interface IRegistroBienesRepository
    {
        Task<IEnumerable<RegistroBienes>> GetRegistroBienes();
        Task<RegistroBienes> GetRegistroBienes(int id);
        Task<RegistroBienes> CreateRegistroBienes(RegistroBienes registroBienes);
        Task<RegistroBienes> UpdateRegistroBienes(RegistroBienes registroBienes);
        Task<RegistroBienes> DeleteRegistroBienes(int id);

        Task<RegistroBienes> GetRegistroBienesByCodigoBien(string codigoBien);

        Task<IEnumerable<RegistroBienes>> GetRegistroBienesByUnidadAdministrativaId(int unidadAdministrativaId);
        Task<IEnumerable<RegistroBienes>> GetRegistroBienesByCriteria(int unidadAdministrativaId, int? estadoId = null, int? partidaId = null, int? estatusId = null, int? cambId = null, string? contrato = null, int? cucopId = null);
        Task<IEnumerable<ContadorBienesPorUnidadDto>> ObtenerConteosPorUnidadAsync();
    }


}
