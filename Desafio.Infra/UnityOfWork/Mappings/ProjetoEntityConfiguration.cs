using Desafio.Domain1.ProjetoModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infra.UnityOfWork.Mappings
{
    public class ProjetoEntityConfiguration : IEntityTypeConfiguration<Projeto>
    {
        public void Configure(EntityTypeBuilder<Projeto> builder)
        {
            builder.Property(x => x.Titulo)
                .HasMaxLength(255);

            builder.Property(x => x.Prazo)
                .HasColumnType("Date");


            builder.ToTable("Projeto", "dbo");
        }
    }
}
