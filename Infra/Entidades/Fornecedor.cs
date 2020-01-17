using Infra.Enums;
using System;
using System.Collections.Generic;

namespace Infra
{
    public class Fornecedor
    {
        public Guid Id { get;  set; }
        public string Nome { get;  set; }
        public string Documento { get;  set; }

        public TipoFornecedor TipoFornecedor { get;  set; }

        public bool Ativo { get;  set; }
        public DateTime DataCadastro { get;  set; }

        public Fornecedor()
        {
            Id = Guid.NewGuid();
        }

        public virtual IEnumerable<Endereco> Enderecos { get; set; }
        public virtual IEnumerable<Produto> Produtos { get; set; }
    }
}
