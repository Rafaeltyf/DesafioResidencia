using Desafio.Application.DTO1.TarefaAgg;
using Desafio.Application1.ProjetoModule;
using Desafio.Application1.TarefaModule;
using Microsoft.AspNetCore.Mvc;

namespace DesafioResidencia.Controllers
{
    [Route("api/[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaAppService _tarefaAppService;

        public TarefaController(ITarefaAppService tarefaAppService)
        {
            if (tarefaAppService == null)
                throw new ArgumentNullException();
            _tarefaAppService = tarefaAppService;
        }

        [HttpGet("ObterTarefa")]
        public TarefaDTO GetTarefa(long id)
        {
            return _tarefaAppService.ObterTarefa(id);
        }

        [HttpGet("ListarTarefas")]
        public async Task<ActionResult<TarefaCountDTO>> GetTarefas(TarefaFiltroDTO filtro)
        {
            try
            {
                var all = _tarefaAppService.ListarTarefas(filtro);
                return all;
            }
            catch
            {
                throw new ArgumentException();
            }
        }

        [HttpPost("AdicionarTarefa")]
        public async Task<ActionResult<TarefaDTO>> Create([FromBody] TarefaDTO tarefaDTO)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException();
            }
            return _tarefaAppService.AdicionarTarefa(tarefaDTO);
        }

        [HttpPut("EditarTarefa")]
        public void Edit([FromBody] TarefaDTO tarefaDTO)
        {
            if(tarefaDTO == null)
                throw new ArgumentNullException();

            _tarefaAppService.EditarTarefa(tarefaDTO);
        }

        [HttpDelete("RemovePessoa")]
        public void Delete(long id)
        {
            this._tarefaAppService.RemoverTarefa(id);
        }
    }
}
