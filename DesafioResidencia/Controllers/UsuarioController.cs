using Desafio.Application.DTO1.UsuarioAgg;
using Desafio.Application1.UsuarioModule;
using Microsoft.AspNetCore.Mvc;

namespace DesafioResidencia.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public UsuarioController(IUsuarioAppService usuarioAppService)
        {
            if (usuarioAppService == null)
                throw new ArgumentNullException();

            _usuarioAppService = usuarioAppService;
        }

        [HttpGet("ObterUsuario")]
        public UsuarioDTO GetUsuario(long id)
        {
            return _usuarioAppService.ObterUsuario(id);
        }

        [HttpGet("ListarUsuarios")]
        public async Task<ActionResult<UsuarioCountDTO>> GetUsuarios(UsuarioFiltroDTO filtro)
        {
            try
            {
                var all = _usuarioAppService.ListarUsuarios(filtro);
                return all;
            }
            catch
            {
                throw new ArgumentException();
            }
        }

        [HttpPost("AdicionarUsuario")]
        public async Task<ActionResult<UsuarioDTO>> Create([FromBody] UsuarioDTO usuariooDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException();
            }
            return _usuarioAppService.AdicionarUsuario(usuariooDTO);
        }

        [HttpPut("EditarUsuario")]
        public void Edit([FromBody] UsuarioDTO usuariooDTO)
        {
            if (usuariooDTO == null)
                throw new ArgumentNullException();

            _usuarioAppService.EditarUsuario(usuariooDTO);
        }

        [HttpDelete("RemoverUsuario")]
        public void Delete(long id)
        {
            _usuarioAppService.RemoverUsuario(id);
        }
    }
}
