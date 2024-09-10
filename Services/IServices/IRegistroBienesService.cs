﻿using ActivoFijo.Dtos;
using ActivoFijo.Dtos.ActivoFijo.Dtos;


namespace ActivoFijo.Services.IServices
{
    public interface IRegistroBienesService
    {

        Task<List<RegistroBienesTempDto>> CargarExcelAsync(IFormFile archivo);

        Task<List<RegistroBienesDto>> GetRegistroBienesByUnidadAdministrativaId(int unidadAdministrativaId);
        Task<IEnumerable<RegistroBienesDto>> GetRegistroBienesByCriteria(int unidadAdministrativaId, int? estadoId = null, int? partidaId = null, int? estatusId = null, int? cambId = null, string? contrato = null, int? cucopId = null);


        Task<RegistroBienesDto> CreaRegistroBien(CreaRegistroBienesDto registroBienesDto);
        Task<RegistroBienesDto> ActualizaRegistroBien(ActualizaRegistroBienesDto registroBienesDto);
        Task<RegistroBienesDto> EliminaRegistroBien(int registroBienesId);

        Task<RegistroBienesDto> GetRegistroBienes(int id);
        Task<IEnumerable<ContadorBienesPorUnidadDto>> ObtenerConteosPorUnidadAsync();

    }

}
