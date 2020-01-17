using System;

namespace Infra
{
    public class Endereco
    {

        public Guid Id { get; private set; }

        public Endereco()
        {
            Id = Guid.NewGuid();
        }

        public string Descricao { get; private set; }

        public Guid FornecedorId { get; private set; }
        public virtual Fornecedor Fornecedor { get; private set; }

    }
}
