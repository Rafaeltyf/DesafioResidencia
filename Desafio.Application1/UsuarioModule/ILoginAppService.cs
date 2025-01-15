using Desafio.Application.DTO1.UsuarioAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Application1.UsuarioModule
{
    public interface ILoginAppService
    {
        Task<LoginDTO> EfetuarLogin(string login, string senha);
    }
}
