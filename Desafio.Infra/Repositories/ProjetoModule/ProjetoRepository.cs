using Desafio.Domain1.ProjetoModule;
using Desafio.Domain1.UsuarioModule;
using Desafio.Infra.UnityOfWork;
using Infrastructure.Data.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infra.Repositories.ProjetoModule
{
    public class ProjetoRepository : Repository<Projeto>, IProjetoRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public ProjetoRepository(AppDbContext unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion

        public override Projeto Get(long id)
        {
            if (id != 0)
            {
                var currentUnitOfWork = this.UnitOfWork as AppDbContext;

                var set = currentUnitOfWork.CreateSet<Projeto>();

                return set.Where(x => x.Id == id)
                          .SingleOrDefault();
            }
            else
                return null;
        }
    }
}
