using Business.Documentos;
using FluentValidation;
using Infra;
using Infra.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Validacoes
{
    public class FornecedorValidacao: AbstractValidator<Fornecedor>
    {
        public FornecedorValidacao()
        {
            RuleFor(expression: f => f.Nome)
                 .NotEmpty().WithMessage("O campo nome precisa ser fornecido")
                 .Length(min: 2, max: 100)
                 .WithMessage("O campo nome precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(predicate: f => f.TipoFornecedor == TipoFornecedor.PessoaFisica, action: () =>
            {
                RuleFor(expression: f => f.Documento.Length).Equal(toCompare: CpfValidacao.TamanhoCpf)
                        .WithMessage("O campo documento precisa ser {ComparisonValue} caracteres");

                RuleFor(expression: f => CpfValidacao.Validar(f.Documento)).Equal(toCompare: true)
                        .WithMessage("O documento fornecido é inválido");

            });

            When(predicate: f => f.TipoFornecedor == TipoFornecedor.PessoaJuridica, action: () =>
            {
                RuleFor(expression: f => f.Documento.Length).Equal(toCompare: CnpjValidacao.TamanhoCnpj)
                        .WithMessage("O campo documento precisa ser {ComparisonValue} caracteres");

                RuleFor(expression: f => CnpjValidacao.Validar(f.Documento)).Equal(toCompare: true)
                        .WithMessage("O documento fornecido é inválido");

            });
        }
    }
}
