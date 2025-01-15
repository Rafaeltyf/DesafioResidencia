using Desafio.Domain1.TarefaModule;
using Desafio.Domain1.UsuarioModule;
using Domain.Seedwork1;

namespace Desafio.Domain1.ProjetoModule
{
    public class Projeto : Entity
    {
        public string Titulo { get; set; }
        public DateTime Prazo { get; set; }
    }

}
