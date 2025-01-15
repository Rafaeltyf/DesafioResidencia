using Desafio.Application.DTO1.ProjetoAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Application.DTO1.TarefaAgg
{
    public class TarefaDTO
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public long ProjetoId { get; set; }
        public ProjetoDTO Projeto { get; set; }
    }
}
