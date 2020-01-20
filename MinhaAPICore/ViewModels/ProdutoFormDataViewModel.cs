using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaAPICore.ViewModels
{
    public class ProdutoFormDataViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Descricao { get; set; }

        public IFormFile ImagemUpload { get; set; }
        public string Imagem { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro  { get; set; }

        [ScaffoldColumn(false)]
        public string NomeFornecedor { get; set; }

        [Required(ErrorMessage = "Informe o fornecedor")]
        public Guid FornecedorId { get; set; }

        //ScaffoldColumn não leva este atributo para a página razor
    }
}
