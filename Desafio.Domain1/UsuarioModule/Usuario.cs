using Desafio.Domain1.ProjetoModule;
using Domain.Seedwork1;

namespace Desafio.Domain1.UsuarioModule
{
    public class Usuario : Entity
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public long ProjetoId { get; set; }
        public Projeto Projeto {  get; set; }
    }
}
