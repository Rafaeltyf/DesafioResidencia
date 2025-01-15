using Desafio.Domain1.ProjetoModule;
using Desafio.Domain1.TarefaModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infra.UnityOfWork.Mappings
{
    public class TarefaEntityConfiguration : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.Property(x => x.Titulo)
                .HasMaxLength(255);

            builder.Property(x => x.Descricao)
                .HasMaxLength(255);

            builder.ToTable("Tarefa", "dbo");
        }
    }
}
