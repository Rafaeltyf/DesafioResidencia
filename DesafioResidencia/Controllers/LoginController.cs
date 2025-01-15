using Desafio.Application.DTO1.UsuarioAgg;
using Desafio.Application1.UsuarioModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioResidencia.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        readonly ILoginAppService _loginAppService;
        readonly IUsuarioAppService _usuarioAppService;

        public LoginController(IUsuarioAppService usuarioAppService, ILoginAppService loginAppService)
        {
            _loginAppService = loginAppService;
            _usuarioAppService = usuarioAppService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginDTO>> PostLogin([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null) { BadRequest("Dados informados inválidos."); }

            return await _loginAppService.EfetuarLogin(loginDTO.Login, loginDTO.Senha);
        }

        [HttpPost("logout")]
        public void PostLogout()
        {
            return;
        }
    }
}
