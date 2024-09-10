using ActivoFijo.Models;

namespace ActivoFijo.Repositories.IRepository
{
    public interface IRegistroBienesTempRepository
    {
        Task AddRangeAsync(IEnumerable<RegistroBienesTemp> registros);

    }
}
