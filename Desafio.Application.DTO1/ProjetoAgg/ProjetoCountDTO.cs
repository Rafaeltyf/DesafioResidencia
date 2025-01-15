using Desafio.Application.DTO1.TarefaAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Application.DTO1.ProjetoAgg
{
    public class ProjetoCountDTO
    {
        public long Count { get; set; }
        public IEnumerable<ProjetoDTO> Projetos { get; set; }
    }
}
