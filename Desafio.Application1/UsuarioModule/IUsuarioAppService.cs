using Desafio.Application.DTO1.UsuarioAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Application1.UsuarioModule
{
    public interface IUsuarioAppService
    {
        UsuarioDTO ObterUsuario(long id);
        UsuarioCountDTO ListarUsuarios(UsuarioFiltroDTO filtro);
        UsuarioDTO AdicionarUsuario(UsuarioDTO usuarioDTO);
        void EditarUsuario(UsuarioDTO usuarioDTO);
        void RemoverUsuario(long id);
    }
}
