using Desafio.Domain1.TarefaModule;
using Desafio.Domain1.UsuarioModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Application.DTO1.ProjetoAgg
{
    public class ProjetoDTO
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public DateTime Prazo { get; set; }
    }
}
