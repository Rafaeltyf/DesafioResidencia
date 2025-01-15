using AutoMapper;
using Desafio.Application.DTO1.UsuarioAgg;
using Desafio.Domain1.UsuarioModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Application.DTO1.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile() 
        {
            var usuarioMapping = CreateMap<Usuario, UsuarioDTO>();
            usuarioMapping.ForMember(dto => dto.Id, x => x.MapFrom(y => y.Id));
            usuarioMapping.ForMember(dto => dto.Login, x => x.MapFrom(y => y.Login));
            usuarioMapping.ForMember(dto => dto.Senha, x => x.MapFrom(y => y.Senha));
            usuarioMapping.ForMember(dto => dto.Nome, x => x.MapFrom(y => y.Nome));
            usuarioMapping.ForMember(dto => dto.ProjetoId, x => x.MapFrom(y => y.ProjetoId));
        }
    }
}
