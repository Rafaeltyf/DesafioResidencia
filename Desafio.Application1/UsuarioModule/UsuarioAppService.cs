using AutoMapper;
using Desafio.Application.DTO1.ProjetoAgg;
using Desafio.Application.DTO1.UsuarioAgg;
using Desafio.Domain1.UsuarioModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Desafio.Application1.UsuarioModule
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioAppService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public UsuarioDTO ObterUsuario(long id)
        {
            var usuario = _usuarioRepository.Get(id);

            if (usuario == null)
            {
                throw new ArgumentException("Usuário não encontrado.");
            }

            return _mapper.Map<Usuario, UsuarioDTO>(usuario);
        }

        public UsuarioCountDTO ListarUsuarios(UsuarioFiltroDTO filtro)
        {
            var countUsuarios = _usuarioRepository.GetCountPaged();

            var usuariosDTO = _usuarioRepository.GetPaged(filtro.PageIndex, filtro.PageCount, x => x.Nome, true)
                .Select(usuario => new UsuarioDTO()
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Login = usuario.Login,
                    Senha = usuario.Senha,
                    ProjetoId = usuario.ProjetoId,
                    Projeto = new ProjetoDTO()
                    {
                        Id = usuario.ProjetoId,
                        Titulo = usuario.Projeto.Titulo,
                        Prazo = usuario.Projeto.Prazo
                    }
                });

            return new UsuarioCountDTO() { Count = countUsuarios, Usuarios = usuariosDTO };
        }

        public UsuarioDTO AdicionarUsuario(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
                throw new ArgumentNullException("Não é possível adicionar usuário com informações vazias.");

            var usuario = MaterializaUsuarioFromDto(usuarioDTO);

            using (var scope = new TransactionScope())
            {
                if (usuario.Id != 0)
                {
                    var usuarioPersistido = _usuarioRepository.Get(usuario.Id);

                    _usuarioRepository.Merge(usuarioPersistido, usuario);

                    usuario = _usuarioRepository.Get(usuario.Id);
                }

                SalvarUsuario(usuario);

                scope.Complete();
            }

            return usuarioDTO;
        }

        public void EditarUsuario(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null || usuarioDTO.Id == 0)
                throw new ArgumentNullException("Não é possível editar usuário com informações vazias.");

            var persistido = _usuarioRepository.Get(usuarioDTO.Id);

            if (persistido != null)
            {
                var corrente = MaterializaUsuarioFromDto(usuarioDTO);

                using (var scope = new TransactionScope())
                {
                    _usuarioRepository.Merge(persistido, corrente);
                    _usuarioRepository.UnitOfWork.Commit();
                    scope.Complete();
                }
            }
        }

        public void RemoverUsuario(long id)
        {
            var usuario = _usuarioRepository.Get(id);

            if (usuario != null)
            {
                _usuarioRepository.Remove(usuario);
                _usuarioRepository.UnitOfWork.Commit();
            }
            else
            {
                throw new Exception("Usuário não encontrado.");
            }
        }

        private Usuario MaterializaUsuarioFromDto(UsuarioDTO usuarioDTO)
        {
            var usuario = new Usuario()
            {
                Login = usuarioDTO.Login,
                Senha = usuarioDTO.Senha,
                Nome = usuarioDTO.Nome,
                ProjetoId = usuarioDTO.ProjetoId
            };

            usuario.ChangeCurrentIdentity(usuarioDTO.Id);

            return usuario;
        }

        private void SalvarUsuario(Usuario usuario)
        {
            _usuarioRepository.Add(usuario);
            _usuarioRepository.UnitOfWork.Commit();
        }
    }
}
