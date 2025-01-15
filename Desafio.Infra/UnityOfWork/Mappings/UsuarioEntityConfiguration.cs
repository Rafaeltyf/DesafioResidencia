using Desafio.Domain1.ProjetoModule;
using Desafio.Domain1.UsuarioModule;
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
    public class UsuarioEntityConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(u => u.Login)
                .HasMaxLength(256);

            builder.Property(u => u.Senha)
                .HasMaxLength(256);

            builder.Property(u => u.Nome)
                .HasMaxLength(256);

            builder.ToTable("Usuario", "dbo");
        }
    }
}
