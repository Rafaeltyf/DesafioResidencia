using AutoMapper;
using Desafio.Application.DTO1.TarefaAgg;
using Desafio.Domain1.TarefaModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Desafio.Application.DTO1.Profiles
{
    public class TarefaProfile : Profile
    {
        public TarefaProfile() 
        {
            var tarefaMapping = CreateMap<Tarefa, TarefaDTO>();
            tarefaMapping.ForMember(dto => dto.Id, x => x.MapFrom(y => y.Id));
            tarefaMapping.ForMember(dto => dto.Titulo, x => x.MapFrom(y => y.Titulo));
            tarefaMapping.ForMember(dto => dto.Descricao, x => x.MapFrom(y => y.Descricao));
            tarefaMapping.ForMember(dto => dto.ProjetoId, x => x.MapFrom(y => y.ProjetoId));
        }
    }
}
