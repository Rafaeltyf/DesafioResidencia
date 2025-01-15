using AutoMapper;
using Desafio.Application.DTO1.ProjetoAgg;
using Desafio.Domain1.ProjetoModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Application.DTO1.Profiles
{
    public class ProjetoProfile : Profile
    {
        public ProjetoProfile() 
        {
            var projetoMapping = CreateMap<Projeto, ProjetoDTO>();
            projetoMapping.ForMember(dto => dto.Id, x => x.MapFrom(y => y.Id));
            projetoMapping.ForMember(dto => dto.Titulo, x => x.MapFrom(y => y.Titulo));
            projetoMapping.ForMember(dto => dto.Prazo, x => x.MapFrom(y => y.Prazo));     
        }
    }
}
