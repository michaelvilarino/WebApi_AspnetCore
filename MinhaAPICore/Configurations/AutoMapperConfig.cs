using AutoMapper;
using Infra;
using MinhaAPICore.ViewModels;

namespace MinhaAPICore.Configurations
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
        }
    }
}
