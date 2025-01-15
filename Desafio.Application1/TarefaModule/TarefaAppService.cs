using AutoMapper;
using Desafio.Application.DTO1.ProjetoAgg;
using Desafio.Application.DTO1.TarefaAgg;
using Desafio.Domain1.ProjetoModule;
using Desafio.Domain1.TarefaModule;
using Desafio.Domain1.UsuarioModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Desafio.Application1.TarefaModule
{
    public class TarefaAppService : ITarefaAppService
    {

        ITarefaRepository _tarefaRepository;
        IMapper _mapper;

        public TarefaAppService(ITarefaRepository tarefaRepository, IMapper mapper)
        {
            _tarefaRepository = tarefaRepository;
            _mapper = mapper;
        }

        public TarefaDTO ObterTarefa(long id)
        {
            var tarefa = _tarefaRepository.Get(id);

            if(tarefa == null)
            {
                throw new ArgumentException();
            }
            return _mapper.Map<Tarefa, TarefaDTO>(tarefa);
        }

        public TarefaCountDTO ListarTarefas(TarefaFiltroDTO filtro)
        {
            var countTarefas = _tarefaRepository.GetCountPaged();

            var tarefasDTO = _tarefaRepository.GetPaged(filtro.PageIndex, filtro.PageCount, x => x.Titulo, true)
                .Select(tarefa => new TarefaDTO()
                {
                    Id = tarefa.Id,
                    Titulo = tarefa.Titulo,
                    Descricao = tarefa.Descricao,
                    ProjetoId = tarefa.ProjetoId,
                    Projeto = new ProjetoDTO()
                    {
                        Id = tarefa.ProjetoId,
                        Titulo = tarefa.Projeto.Titulo,
                        Prazo = tarefa.Projeto.Prazo
                    }
                });

            return new TarefaCountDTO() { Count = countTarefas, Tarefas = tarefasDTO };
        }

        public TarefaDTO AdicionarTarefa(TarefaDTO tarefaDTO)
        {
            if (tarefaDTO == null)
                throw new ArgumentNullException("Não é possível adicionar tarefa com informações vaizias");

            var tarefa = this.MaterializaTarefaFromDto(tarefaDTO);

            using (var scope = new TransactionScope())
            {
                if(tarefa.Id != 0)
                {
                    var tarefaPersistido = _tarefaRepository.Get(tarefa.Id);

                    _tarefaRepository.Merge(tarefaPersistido, tarefa);

                    tarefa = _tarefaRepository.Get(tarefa.Id);
                }

                SalvarTarefa(tarefa);

                scope.Complete();
            }

            return tarefaDTO;
        }

        public void EditarTarefa(TarefaDTO tarefaDTO)
        {
            if (tarefaDTO == null || tarefaDTO.Id == 0)
                throw new ArgumentNullException("Não é possível editar tarefa com informações vazias.");

            var persistido = _tarefaRepository.Get(tarefaDTO.Id);

            if(persistido != null)
            {
                if (persistido.Id != tarefaDTO.Id)
                {
                    persistido = _tarefaRepository.Get(tarefaDTO.Id);
                }

                var corrente = MaterializaTarefaFromDto(tarefaDTO);

                using (var scope = new TransactionScope())
                {
                    _tarefaRepository.Merge(persistido, corrente);
                    _tarefaRepository.UnitOfWork.Commit();
                    scope.Complete();
                }                
            }
        }

        public void RemoverTarefa(long id)
        {
            var tarefa = _tarefaRepository.Get(id);

            if (tarefa != null)
            {
                _tarefaRepository.Remove(tarefa);
                _tarefaRepository.UnitOfWork.Commit();
            }
            else
                throw new Exception();
        }

        private Tarefa MaterializaTarefaFromDto(TarefaDTO tarefaDTO)
        {
            var tarefa = new Tarefa()
            {
                Titulo = tarefaDTO.Titulo,
                Descricao = tarefaDTO.Descricao,
                ProjetoId = tarefaDTO.ProjetoId
            };

            tarefa.ChangeCurrentIdentity(tarefaDTO.Id);

            return tarefa;
        }

        private void SalvarTarefa(Tarefa tarefa)
        {
            _tarefaRepository.Add(tarefa);
            _tarefaRepository.UnitOfWork.Commit();
        }
    }
}
