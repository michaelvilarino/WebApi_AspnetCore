using Infra.Contexto;
using Infra.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repositorios
{
    public class EnderecoRepositorio : BaseRepositorio<Endereco>, IEnderecoRepositorio
    {
        public EnderecoRepositorio(AplicacaoContexto AplicacaoContexto) : base(AplicacaoContexto)
        {

        }
    }
}
