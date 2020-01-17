using Infra.Contexto;
using Infra.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repositorios
{
    public class ProdutoRepositorio : BaseRepositorio<Produto>, IProdutoRepositorio
    {
        public ProdutoRepositorio(AplicacaoContexto AplicacaoContexto) : base(AplicacaoContexto)
        {
        }
    }
}
