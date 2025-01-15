using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Application.DTO1.TarefaAgg
{
    public class TarefaCountDTO
    {
        public long Count { get; set; }

        public IEnumerable<TarefaDTO> Tarefas { get; set; }
    }
}
