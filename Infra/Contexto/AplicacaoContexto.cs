using Infra.Mapeamentos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infra.Contexto
{
    public class AplicacaoContexto : DbContext
    {
        private string _connectionString = "";

        public AplicacaoContexto(DbContextOptions options, IConfiguration configuration) : base(options){

            _connectionString = configuration.GetConnectionString("ConexaoBanco");            
        }

        public DbSet<Endereco> enderecos { get; set; }
        public DbSet<Fornecedor> fornecedores { get; set; }
        public DbSet<Produto> produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.ApplyConfiguration(new FornecedorMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new EnderecoMap());

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{            
        //    optionsBuilder.UseSqlServer(_connectionString);
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
                                                                      optionsBuilder
                                                                     .UseLazyLoadingProxies()
                                                                     .UseSqlServer(_connectionString);
    }
}
