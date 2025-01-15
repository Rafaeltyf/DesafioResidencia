using Desafio.Application.DTO1.TarefaAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Application1.TarefaModule
{
    public interface ITarefaAppService
    {
        TarefaDTO ObterTarefa(long id);
        TarefaCountDTO ListarTarefas(TarefaFiltroDTO filtro);
        TarefaDTO AdicionarTarefa(TarefaDTO tarefaDTO);
        void EditarTarefa(TarefaDTO tarefaDTO);
        void RemoverTarefa(long id);
    }
}
