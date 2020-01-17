using FluentValidation;
using Infra;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Validacoes
{
    public class ProdutoValidacao: AbstractValidator<Produto>
    {
        public ProdutoValidacao()
        {
            RuleFor(f => f.Fornecedor).NotNull().WithMessage("Fornecedor não foi informado!");
            RuleFor(f => f.Descricao).NotEmpty().WithMessage("Informe a descrição do produto");
        }
    }
}
