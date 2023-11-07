using AutoMapper;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.DTO
{
    public class UsuarioDto
    {
        public string Correo { get; set; }
        public string Contra { get; set; }
        public int ID_Rol { get; set; }
    }
    public class UsuarioMapper : Profile
    {
        public UsuarioMapper()
        {
            CreateMap<UsuarioDto, Usuario>()
                .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Correo))
                .ForMember(dest => dest.Contra, opt => opt.MapFrom(src => src.Contra))
                .ForMember(dest => dest.ID_Rol, opt => opt.MapFrom(src => src.ID_Rol))
                .ForMember(dest => dest.rol, opt => opt.Ignore());
        }
    }
}
