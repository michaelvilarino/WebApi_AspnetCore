﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Servicos.Interfaces;
using Infra;
using Infra.Repositorios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinhaAPICore.Controllers;
using MinhaAPICore.Extensoes;
using MinhaAPICore.ViewModels;

namespace MinhaAPICore.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/fornecedores")]
    public class FornecedoresController : MainController
    {
        private readonly IFornecedorRepositorio _fornecedorRepositorio;
        private readonly IFornecedorAppServico _fornecedorAppServico;
        private readonly IMapper _mapper;
        public FornecedoresController
        (
            IFornecedorRepositorio IFornecedorRepositorio, 
            IMapper mapper, 
            IFornecedorAppServico IFornecedorAppServico,
            INotificador notificador,
            IUser appuser
        ) :base(notificador, appuser)
        {
            _fornecedorRepositorio = IFornecedorRepositorio;
            _mapper = mapper;
            _fornecedorAppServico = IFornecedorAppServico;
        }

        [AllowAnonymous]
        [HttpGet("ObterTodos")]
        public async Task<IEnumerable<FornecedorViewModel>> ObterTodos()
        {
            var fornecedor = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepositorio.ObterTodos());

            return fornecedor;
        }

        [HttpGet("ObterPorId/{Id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> ObterPorId(Guid Id)
        {
            var fornecedor = await ObterFornecedorProdutosEndereco(Id);

            if (fornecedor == null) return NotFound();

            return fornecedor;
        }

        [ClaimsAuthorize("Fornecedor", "Adicionar")]
        [HttpPost]
        public async Task<ActionResult<FornecedorViewModel>> Adicionar(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);

            fornecedor.DataCadastro = DateTime.Now;

            await _fornecedorAppServico.Adicionar(fornecedor);

            return CustomResponse(fornecedorViewModel);
        }

        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        [HttpPut("Atualizar/{Id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Atualizar(Guid Id, FornecedorViewModel fornecedorViewModel)
        {
            if (Id != fornecedorViewModel.Id)
            {
                NotificarErro("O id informado é inválido");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _fornecedorAppServico.Atualizar(_mapper.Map<Fornecedor>(fornecedorViewModel));

            return CustomResponse(fornecedorViewModel);
        }

        [ClaimsAuthorize("Fornecedor", "Remover")]
        [HttpDelete("Excluir/{Id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Excluir(Guid Id)
        {
            var fornecedor = await _fornecedorRepositorio.ObterPorId(Id);

            if (fornecedor == null) return NotFound();

            await _fornecedorAppServico.Remover(fornecedor);

            return CustomResponse();
        }


        private  async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid Id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepositorio.ObterFornecedorProdutosEndereco(Id));
        }
        
        private  async Task<ActionResult<FornecedorViewModel>> ObterFornecedorEndereco(Guid Id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepositorio.ObterFornecedorEndereco(Id));
        }
    }
}
