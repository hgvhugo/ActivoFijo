using ActivoFijo.Models;
using ActivoFijo.Repositories;
using ActivoFijo.Repositories.IRepository;
using ActivoFijo.Services;
using ActivoFijo.Services.IServices;
using DinkToPdf.Contracts;
using DinkToPdf;

namespace ActivoFijo.Config
{
    public static class ConfigCustomServices
    {

        public static void AddCustomServices(this IServiceCollection services)
        {
            ///Repositorios
            services.AddScoped<IRepositorioGenerico<Cucop>, RepositorioGenerico<Cucop>>();
            services.AddScoped<IRepositorioGenerico<Partida>, RepositorioGenerico<Partida>>();
            services.AddScoped<IRepositorioGenerico<Contrato>, RepositorioGenerico<Contrato>>();
            services.AddScoped<IRepositorioGenerico<Empleado>, RepositorioGenerico<Empleado>>();
            services.AddScoped<IRepositorioGenerico<UnidadAdministrativa>, RepositorioGenerico<UnidadAdministrativa>>();
            services.AddScoped<IRepositorioGenerico<EmpleadoUnidadAdministrativa>, RepositorioGenerico<EmpleadoUnidadAdministrativa>>();
            services.AddScoped<IRepositorioGenerico<Camb>, RepositorioGenerico<Camb>>();
            services.AddScoped<IRepositorioGenerico<Ubicacion>, RepositorioGenerico<Ubicacion>>();
            //services.AddScoped<IRepositorioGenerico<RegistroBienes>, RepositorioGenerico<RegistroBienes>>();
            services.AddScoped<IRegistroBienesRepository, RegistroBienesRepository>();
            services.AddScoped<IRegistroBienesTempRepository, RegistroBienesTempRepository>();


            ///Servicios

            services.AddAutoMapper(typeof(AppMapping));
            services.AddScoped<IEmpleadoService, EmpleadoService>();
            services.AddScoped<IRegistroBienesService, RegistroBienesService>();
            services.AddScoped<ExcelChunkReaderService>();

            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IFirmaElectronicaService, FirmaElectronicaService>();

            services.AddScoped<ICorreoService, CorreoService>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddTransient<PdfService>();

        }


    }
}
