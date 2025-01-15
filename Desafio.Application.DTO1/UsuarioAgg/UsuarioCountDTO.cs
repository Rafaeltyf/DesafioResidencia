using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Application.DTO1.UsuarioAgg
{
    public class UsuarioCountDTO
    {
        public long Count { get; set; }
        public IEnumerable<UsuarioDTO> Usuarios { get; set; }
    }
}
