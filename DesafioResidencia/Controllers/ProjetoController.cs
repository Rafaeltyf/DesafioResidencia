using Desafio.Application.DTO1.ProjetoAgg;
using Desafio.Application1.ProjetoModule;
using Desafio.Application1.TarefaModule;
using Microsoft.AspNetCore.Mvc;

namespace DesafioResidencia.Controllers
{
    [Route("api/[controller]")]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoAppService _projetoAppService;

        public ProjetoController(IProjetoAppService projetoAppService)
        {
            _projetoAppService = projetoAppService;
        }

        [HttpGet("ObterProjeto")]
        public ProjetoDTO GetProjeto(long id)
        {
            return _projetoAppService.ObterProjeto(id);
        }

        [HttpGet("ListarProjetos")]
        public async Task<ActionResult<ProjetoCountDTO>> GetProjetos(ProjetoFiltroDTO filtro)
        {
            try
            {
                var all = _projetoAppService.ListarProjetos(filtro);
                return all;
            }
            catch
            {
                throw new ArgumentException();
            }
        }

        [HttpPost("AdicionarProjeto")]
        public async Task<ActionResult<ProjetoDTO>> Create([FromBody] ProjetoDTO projetoDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException();
            }
            return _projetoAppService.AdicionarProjeto(projetoDTO);
        }

        [HttpPut("EditarProjeto")]
        public void Edit([FromBody] ProjetoDTO projetoDTO)
        {
            if (projetoDTO == null)
                throw new ArgumentNullException();

            _projetoAppService.EditarProjeto(projetoDTO);
        }

        [HttpDelete("RemoverProjeto")]
        public void Delete(long id)
        {
            _projetoAppService.RemoverProjeto(id);
        }
    }
}
