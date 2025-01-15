using AutoMapper;
using Desafio.Application.DTO1.UsuarioAgg;
using Desafio.Domain1.UsuarioModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Application1.UsuarioModule
{
    public class LoginAppService : ILoginAppService
    {
        IUsuarioRepository _usuarioRepository;
        IMapper _mapper;

        public LoginAppService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public void Dispose()
        {
            _usuarioRepository.Dispose();
        }

        public async Task<LoginDTO> EfetuarLogin(string login, string senha)
        {
            Usuario usuario = _usuarioRepository.GetFiltered(x => x.Login == login).FirstOrDefault();

            if (usuario == null || usuario.Senha != senha)
            {
                throw new AuthenticationException("Login ou senha inválidos.");
            }

            return new LoginDTO
            {
                Login = usuario.Login,
                Nome = usuario.Nome,
            };
        }

    }
}