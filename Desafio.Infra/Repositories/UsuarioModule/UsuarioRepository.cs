using Desafio.Domain1.UsuarioModule;
using Desafio.Infra.UnityOfWork;
using Infrastructure.Data.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infra.Repositories.UsuarioModule
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public UsuarioRepository(AppDbContext unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion

        public override Usuario Get(long id)
        {
            if (id != 0)
            {
                var currentUnitOfWork = this.UnitOfWork as AppDbContext;

                var set = currentUnitOfWork.CreateSet<Usuario>();

                return set.Where(x => x.Id == id)
                          .SingleOrDefault();
            }
            else
                return null;
        }
    }
}
