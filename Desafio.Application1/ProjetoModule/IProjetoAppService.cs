using Desafio.Application.DTO1.ProjetoAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Application1.ProjetoModule
{
    public interface IProjetoAppService
    {
        ProjetoDTO ObterProjeto(long id);
        ProjetoCountDTO ListarProjetos(ProjetoFiltroDTO filtro);
        ProjetoDTO AdicionarProjeto(ProjetoDTO projetoDTO);
        void EditarProjeto(ProjetoDTO projetoDTO);
        void RemoverProjeto(long id);
    }
}
