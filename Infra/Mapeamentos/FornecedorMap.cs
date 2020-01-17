using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infra.Mapeamentos
{
    public class FornecedorMap: IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("Fornecedor");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Nome).HasColumnName("Nome").HasColumnType("varchar(200)");
            builder.Property(p => p.Documento).HasColumnName("Documento").HasColumnType("varchar(max)");
            builder.Property(p => p.TipoFornecedor).HasColumnName("TipoFornecedor").HasColumnType("int");
            builder.Property(p => p.Ativo).HasColumnName("Ativo").HasColumnType("bit");
            builder.Property(p => p.DataCadastro).HasColumnName("DataCadastro").HasColumnType("DateTime").HasDefaultValue(DateTime.Now);

            builder.HasMany(h => h.Produtos)
                   .WithOne(w => w.Fornecedor)
                   .HasForeignKey(f => f.FornecedorId);

            builder.HasMany(h => h.Enderecos)
                   .WithOne(w => w.Fornecedor)
                   .HasForeignKey(f => f.FornecedorId);
        }
    }
}
