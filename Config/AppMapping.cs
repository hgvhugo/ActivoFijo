﻿using ActivoFijo.Dtos;
using ActivoFijo.Models;
using AutoMapper;

namespace ActivoFijo.Config
{
    public class AppMapping : Profile
    {
        public AppMapping()
        {
            CreateMap<Camb, CambDto>().ReverseMap();
            CreateMap<Cucop, CucopDto>().ReverseMap();
            //CreateMap<Empleado, EmpleadoDto>().ReverseMap();
            CreateMap<Empleado, EmpleadoDto>()
               .ForMember(dest => dest.Ubicacion, opt => opt.MapFrom(src => src.Ubicacion))
               .ForMember(dest => dest.EmpleadoUnidadesAdministrativas, opt => opt.MapFrom(src => src.EmpleadoUnidadesAdministrativas))
               .ReverseMap();


            CreateMap<Empleado, CreaEmpleadoDto>().ReverseMap();
            CreateMap<EmpleadoUnidadAdministrativa, EmpleadoUnidadAdministrativaDto>().ReverseMap();
            

            CreateMap<FotosBienActivo, FotoBienActivoDto>().ReverseMap();
            CreateMap<Partida, PartidaDto>().ReverseMap();
            CreateMap<RegistroBienes, RegistroBienesDto>().ReverseMap();
            CreateMap<Ubicacion, UbicacionDto>().ReverseMap();            
            CreateMap<UnidadAdministrativa, UnidadAdministrativaDto>().ReverseMap();

            CreateMap<RegistroBienesTemp, RegistroBienesTempDto>().ReverseMap();
            CreateMap<RegistroBienes, RegistroBienesDto>().ReverseMap();
            CreateMap<CreaRegistroBienesDto, RegistroBienes>();
            CreateMap<RegistroBienes, RegistroBienesLog>();


        }
    }
}
