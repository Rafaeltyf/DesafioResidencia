using Desafio.Domain1.TarefaModule;
using Desafio.Domain1.UsuarioModule;
using Desafio.Infra.UnityOfWork;
using Infrastructure.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infra.Repositories.TarefaModule
{
    public class TarefaRepository : Repository<Tarefa>, ITarefaRepository
    {

        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public TarefaRepository(AppDbContext unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion

        public override Tarefa Get(long id)
        {
            if (id != 0)
            {
                var currentUnitOfWork = this.UnitOfWork as AppDbContext;

                var set = currentUnitOfWork.CreateSet<Tarefa>();

                return set.Where(x => x.Id == id)
                          .SingleOrDefault();
            }
            else
                return null;
        }
    }
}
