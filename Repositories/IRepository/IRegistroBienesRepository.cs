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
    }
}
