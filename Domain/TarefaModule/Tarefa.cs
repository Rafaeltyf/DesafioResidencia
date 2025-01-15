using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TarefaModule
{
    public class Tarefa : Entity
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
    }
}
