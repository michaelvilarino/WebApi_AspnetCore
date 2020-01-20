using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapeamentos
{
    public class ProdutoMap: IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Descricao).HasColumnName("Descricao").HasColumnType("varchar(100)");
            builder.Property(p => p.Valor).HasColumnName("Valor").HasColumnType("decimal");
            builder.Property(p => p.Imagem).HasColumnName("Imagem").HasColumnType("varchar(100)");
            builder.Property(p => p.DataCadastro).HasColumnName("DataCadastro").HasColumnType("DateTime");
        }
    }
}
