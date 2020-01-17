using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Mapeamentos
{
    public class EnderecoMap: IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Endereco");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Descricao).HasColumnName("Descricao").HasColumnType("varchar(200)");

        }
    }
}
