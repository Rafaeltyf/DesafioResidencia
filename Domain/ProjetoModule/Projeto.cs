using Desafio.Domain.UsuarioModule;
using Domain.Seedwork;
using Domain.TarefaModule;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProjetoModule
{
    public class Projeto : Entity
    {
        public string Titulo { get; set; }
        public List<Tarefa> Tarefas { get; set; }
        public DateTime Prazo { get; set; }
        public List<Usuario> Responsaveis { get; set; }
    }
}
