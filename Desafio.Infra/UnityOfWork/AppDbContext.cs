using Desafio.Domain1.ProjetoModule;
using Desafio.Domain1.TarefaModule;
using Desafio.Domain1.UsuarioModule;
using Desafio.Infra.UnityOfWork.Mappings;
using Infrastructure.Data.Seedwork;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;

namespace Desafio.Infra.UnityOfWork
{
    public class AppDbContext : IdentityDbContext, IQueryableUnitOfWork
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        DbSet<Usuario> _usuarios;
        public DbSet<Usuario> Usuarios 
        { 
            get { return _usuarios ?? base.Set<Usuario>(); } 
        }

        DbSet<Tarefa> _tarefas;
        public DbSet<Tarefa> Tarefas
        {
            get { return _tarefas ?? base.Set<Tarefa>(); }
        }

        DbSet<Projeto> _projetos;
        public DbSet<Projeto> Projetos
        {
            get { return _projetos ?? base.Set<Projeto>(); }
        }

        #region IQueryableUnitOfWork Members

        public DbSet<TEntity> CreateSet<TEntity>()
            where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void Attach<TEntity>(TEntity item)
            where TEntity : class
        {
            //attach and set as unchanged
            base.Entry<TEntity>(item).State = EntityState.Unchanged;
        }

        public void SetModified<TEntity>(TEntity item)
            where TEntity : class
        {
            //this operation also attach item in object state manager
            base.Entry<TEntity>(item).State = EntityState.Modified;
        }
        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current)
            where TEntity : class
        {
            //if it is not attached, attach original and set current values
            base.Entry<TEntity>(original).CurrentValues.SetValues(current);
        }

        public void Commit()
        {
            try
            {
                base.SaveChanges();
            }
            catch
            {
                throw new AuthenticationException("Ação inválida.");
            }
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });

                }
            } while (saveFailed);

        }

        public void RollbackChanges()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = EntityState.Unchanged);
        }


        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlRaw(sqlCommand, parameters);
        }

        public int ExecuteCommand(string sqlCommand)
        {
            return base.Database.ExecuteSqlRaw(sqlCommand);
        }

        #endregion

        #region DbContext Overrides

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TarefaEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProjetoEntityConfiguration());
            

            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
        #endregion
    }


}
