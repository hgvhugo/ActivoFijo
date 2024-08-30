using ActivoFijo.Models;
using ActivoFijo.Repositories;
using ActivoFijo.Repositories.IRepository;
using ActivoFijo.Services;
using ActivoFijo.Services.IServices;

namespace ActivoFijo.Config
{
    public static class ConfigCustomServices
    {

        public static void AddCustomServices(this IServiceCollection services)
        {
            ///Repositorios
            //services.AddScoped<IRegistroBienesRepository, IRegistroBienesRepository>();
            services.AddScoped<IRepositorioGenerico<Cucop>, RepositorioGenerico<Cucop>>();
            services.AddScoped<IRepositorioGenerico<Partida>, RepositorioGenerico<Partida>>();
            services.AddScoped<IRepositorioGenerico<Contrato>, RepositorioGenerico<Contrato>>();
            services.AddScoped<IRepositorioGenerico<Empleado>, RepositorioGenerico<Empleado>>();

            ///Servicios
            
            services.AddAutoMapper(typeof(AppMapping));
            services.AddScoped<IEmpleadoService, EmpleadoService>();

        }


    }
}
