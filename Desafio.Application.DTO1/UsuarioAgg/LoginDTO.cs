using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Application.DTO1.UsuarioAgg
{
    public class LoginDTO
    {
        public long? Id { get; set; }
        public string? Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
