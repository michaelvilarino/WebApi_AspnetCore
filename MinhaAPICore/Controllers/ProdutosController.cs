using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Servicos.Interfaces;
using Infra;
using Infra.Repositorios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MinhaAPICore.ViewModels;

namespace MinhaAPICore.Controllers
{
    [Route("api/Produtos")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IProdutoAppServico _produtoAppServico;
        private readonly IMapper _mapper;

        public ProdutosController(INotificador notificador,
                                    IProdutoRepositorio IProdutoRepositorio,
                                    IProdutoAppServico IProdutoAppServico,
                                    IMapper IMapper
                                 ) : base(notificador)
        {
            _produtoRepositorio = IProdutoRepositorio;
            _produtoAppServico = IProdutoAppServico;
            _mapper = IMapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            try
            {
                return _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepositorio.ObterTodos());                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> ObterPorId(Guid Id)
        {
            var produtoViewModel = await ObterProduto(Id);

            if (produtoViewModel == null) return NotFound();

            return produtoViewModel;
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> Adicionar(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imagemNome = Guid.NewGuid() + "_" + produtoViewModel.Imagem;

            if (!UploadDeArquivo(produtoViewModel.ImagemUpload, imagemNome))
            {
                return CustomResponse(produtoViewModel);
            }

            produtoViewModel.Imagem = imagemNome;

            await _produtoAppServico.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            return CustomResponse(produtoViewModel);
        }


        [HttpDelete("Id:guid")]
        public async Task<ActionResult<ProdutoViewModel>> Excluir(Guid Id)
        {
            var produto = await _produtoRepositorio.ObterPorId(Id);

            if (produto == null) return NotFound();

            await _produtoAppServico.Remover(produto);

            return CustomResponse();
        }

        private bool UploadDeArquivo(string arquivoBase64, string nomeImagem)
        {
            if (string.IsNullOrEmpty(arquivoBase64))
            {
                NotificarErro("fornceça a imagem!");
                return false;
            }

            var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Imagens", nomeImagem);

            if (System.IO.Directory.Exists(caminhoArquivo))
            {
                NotificarErro("A imagem já existe!");
                return false;
            }

            var bytesImagem = Convert.FromBase64String(arquivoBase64);

            System.IO.File.WriteAllBytes(caminhoArquivo, bytesImagem);

            return true;
        }

        private async Task<ProdutoViewModel> ObterProduto(Guid Id)
        { 
           return _mapper.Map<ProdutoViewModel>(await _produtoRepositorio.ObterPorId(Id));
        }
    }
}