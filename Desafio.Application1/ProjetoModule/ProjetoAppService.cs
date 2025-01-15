using AutoMapper;
using Desafio.Application.DTO1.ProjetoAgg;
using Desafio.Domain1.ProjetoModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Desafio.Application1.ProjetoModule
{
    public class ProjetoAppService : IProjetoAppService
    {

        private readonly IProjetoRepository _projetoRepository;
        private readonly IMapper _mapper;

        public ProjetoAppService(IProjetoRepository projetoRepository, IMapper mapper)
        {
            _projetoRepository = projetoRepository;
            _mapper = mapper;
        }

        public ProjetoDTO ObterProjeto(long id)
        {
            var projeto = _projetoRepository.Get(id);

            if (projeto == null)
            {
                throw new ArgumentException("Projeto não encontrado.");
            }

            return _mapper.Map<Projeto, ProjetoDTO>(projeto);
        }

        public ProjetoCountDTO ListarProjetos(ProjetoFiltroDTO filtro)
        {
            var countProjetos = _projetoRepository.GetCountPaged();

            var projetosDTO = _projetoRepository.GetPaged(filtro.PageIndex, filtro.PageCount, x => x.Titulo, true)
                .Select(projeto => new ProjetoDTO()
                {
                    Id = projeto.Id,
                    Titulo = projeto.Titulo,
                    Prazo = projeto.Prazo
                });

            return new ProjetoCountDTO() { Count = countProjetos, Projetos = projetosDTO };
        }

        public ProjetoDTO AdicionarProjeto(ProjetoDTO projetoDTO)
        {
            if (projetoDTO == null)
                throw new ArgumentNullException("Não é possível adicionar projeto com informações vazias.");

            var projeto = MaterializaProjetoFromDto(projetoDTO);

            using (var scope = new TransactionScope())
            {
                if (projeto.Id != 0)
                {
                    var projetoPersistido = _projetoRepository.Get(projeto.Id);

                    _projetoRepository.Merge(projetoPersistido, projeto);

                    projeto = _projetoRepository.Get(projeto.Id);
                }

                SalvarProjeto(projeto);

                scope.Complete();
            }

            return projetoDTO;
        }

        public void EditarProjeto(ProjetoDTO projetoDTO)
        {
            if (projetoDTO == null || projetoDTO.Id == 0)
                throw new ArgumentNullException("Não é possível editar projeto com informações vazias.");

            var persistido = _projetoRepository.Get(projetoDTO.Id);

            if (persistido != null)
            {
                var corrente = MaterializaProjetoFromDto(projetoDTO);

                using (var scope = new TransactionScope())
                {
                    _projetoRepository.Merge(persistido, corrente);
                    _projetoRepository.UnitOfWork.Commit();
                    scope.Complete();
                }
            }
        }

        public void RemoverProjeto(long id)
        {
            var projeto = _projetoRepository.Get(id);

            if (projeto != null)
            {
                _projetoRepository.Remove(projeto);
                _projetoRepository.UnitOfWork.Commit();
            }
            else
            {
                throw new Exception("Projeto não encontrado.");
            }
        }

        private Projeto MaterializaProjetoFromDto(ProjetoDTO projetoDTO)
        {
            var projeto = new Projeto()
            {
                Titulo = projetoDTO.Titulo,
                Prazo = projetoDTO.Prazo
            };

            projeto.ChangeCurrentIdentity(projetoDTO.Id);

            return projeto;
        }

        private void SalvarProjeto(Projeto projeto)
        {
            _projetoRepository.Add(projeto);
            _projetoRepository.UnitOfWork.Commit();            
        }
    }
}
