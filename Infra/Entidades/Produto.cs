using System;

namespace Infra
{
    public class Produto
    {
        public Guid Id { get; private set; }
        public string Descricao { get; private set; }

        public decimal Valor { get; private set; }

        public string ImagemUpload { get; private set; }
        public string Imagem { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public Guid FornecedorId { get; private set; }
        public virtual Fornecedor Fornecedor { get; private set; }

        public Produto()
        {
            Id = Guid.NewGuid();
        }



    }
}
