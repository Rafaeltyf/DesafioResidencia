using Desafio.Domain1.ProjetoModule;
using Domain.Seedwork1;

namespace Desafio.Domain1.TarefaModule
{
    public class Tarefa : Entity
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public long ProjetoId { get; set;}
        public Projeto Projeto { get; set; }

    }
}