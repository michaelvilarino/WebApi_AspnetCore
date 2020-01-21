using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Business.Servicos.Interfaces;
using Infra;
using Infra.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinhaAPICore.Controllers;
using MinhaAPICore.ViewModels;

namespace MinhaAPICore.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Produtos")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IProdutoAppServico _produtoAppServico;
        private readonly IMapper _mapper;
        private readonly IFornecedorRepositorio _fornecedorRepositorio;

        public ProdutosController(INotificador notificador,
                                    IProdutoRepositorio IProdutoRepositorio,
                                    IProdutoAppServico IProdutoAppServico,
                                    IMapper IMapper,
                                    IFornecedorRepositorio IFornecedorRepositorio,
                                    IUser appuser
                                 ) : base(notificador, appuser)
        {
            _produtoRepositorio = IProdutoRepositorio;
            _produtoAppServico = IProdutoAppServico;
            _mapper = IMapper;
            _fornecedorRepositorio = IFornecedorRepositorio;
        }

        //[HttpGet]
        //public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        //{
        //    try
        //    {
        //        return _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepositorio.ObterTodos());                
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[HttpGet("ObterPorId/{Id:guid}")]
        //public async Task<ActionResult<ProdutoViewModel>> ObterPorId(Guid Id)
        //{
        //    var produtoViewModel = await ObterProduto(Id);

        //    if (produtoViewModel == null) return NotFound();

        //    return produtoViewModel;
        //}

        //[HttpPost]
        //public async Task<ActionResult<ProdutoViewModel>> Adicionar(ProdutoViewModel produtoViewModel)
        //{
        //    if (!ModelState.IsValid) return CustomResponse(ModelState);

        //    var imagemNome = Guid.NewGuid() + "_" + produtoViewModel.Imagem;

        //    if (!UploadDeArquivo(produtoViewModel.ImagemUpload, imagemNome))
        //    {
        //        return CustomResponse(produtoViewModel);
        //    }

        //    produtoViewModel.Imagem = imagemNome;

        //    await _produtoAppServico.Adicionar(_mapper.Map<Produto>(produtoViewModel));

        //    return CustomResponse(produtoViewModel);
        //}

        //[HttpPost("AdicionarFormData")]
        //public async Task<ActionResult<ProdutoViewModel>> AdicionarFormData([FromForm] ProdutoFormDataViewModel produtoViewModel)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid) return CustomResponse(ModelState);

        //        var imagemNome = Guid.NewGuid() + "_";

        //        if (!await UploadImagemFormFile(produtoViewModel.ImagemUpload))
        //        {
        //            return CustomResponse(produtoViewModel);
        //        }

        //        produtoViewModel.Imagem = imagemNome + produtoViewModel.ImagemUpload.FileName;

        //        var novoProduto = _mapper.Map<Produto>(produtoViewModel);                

        //        novoProduto.Fornecedor = new Fornecedor() { Id = produtoViewModel.FornecedorId };

        //        await _produtoAppServico.Adicionar(novoProduto);

        //        return CustomResponse(produtoViewModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //private async Task<bool> UploadImagemFormFile(IFormFile formFile)
        //{
        //    if (formFile == null || formFile.Length == 0)
        //    {
        //        NotificarErro("Imagem não informada!");
        //        return false;

        //    }

        //    var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Imagens", formFile.FileName);

        //    if (System.IO.Directory.Exists(caminhoArquivo))
        //    {
        //        NotificarErro("A imagem já existe!");
        //        return false;
        //    }

        //    using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
        //    {
        //        await formFile.CopyToAsync(stream);
        //    }

        //    return true;
        //}


        //[HttpDelete("Excluir/{Id:guid}")]
        //public async Task<ActionResult<ProdutoViewModel>> Excluir(Guid Id)
        //{
        //    var produto = await _produtoRepositorio.ObterPorId(Id);

        //    if (produto == null) return NotFound();

        //    await _produtoAppServico.Remover(produto);

        //    return CustomResponse();
        //}

        //private bool UploadDeArquivo(string arquivoBase64, string nomeImagem)
        //{
        //    if (string.IsNullOrEmpty(arquivoBase64))
        //    {
        //        NotificarErro("fornceça a imagem!");
        //        return false;
        //    }

        //    var caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Imagens", nomeImagem);

        //    if (System.IO.Directory.Exists(caminhoArquivo))
        //    {
        //        NotificarErro("A imagem já existe!");
        //        return false;
        //    }

        //    var bytesImagem = Convert.FromBase64String(arquivoBase64);

        //    System.IO.File.WriteAllBytes(caminhoArquivo, bytesImagem);

        //    return true;
        //}

        //private async Task<ProdutoViewModel> ObterProduto(Guid Id)
        //{ 
        //   return _mapper.Map<ProdutoViewModel>(await _produtoRepositorio.ObterPorId(Id));
        //}
    }
}