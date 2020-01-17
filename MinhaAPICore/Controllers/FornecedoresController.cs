using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Servicos.Interfaces;
using Infra;
using Infra.Repositorios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MinhaAPICore.ViewModels;

namespace MinhaAPICore.Controllers
{
    [Route("api/[Controller]")]
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
            INotificador notificador
        ):base(notificador)
        {
            _fornecedorRepositorio = IFornecedorRepositorio;
            _mapper = mapper;
            _fornecedorAppServico = IFornecedorAppServico;
        }

        [HttpGet]
        public async Task<IEnumerable<FornecedorViewModel>> ObterTodos()
        {
            try
            {
                var fornecedor = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepositorio.ObterTodos());

                return fornecedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> ObterPorId(Guid Id)
        {
            var fornecedor = await ObterFornecedorProdutosEndereco(Id);

            if (fornecedor == null) return NotFound();

            return fornecedor;
        }

        [HttpPost]
        public async Task<ActionResult<FornecedorViewModel>> Adicionar(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);

            fornecedor.DataCadastro = DateTime.Now;

            await _fornecedorAppServico.Adicionar(fornecedor);

            return CustomResponse(fornecedorViewModel);
        }

        [HttpPost("{Id:guid}")]
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

        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Excluir(Guid Id)
        {
            var fornecedor = await _fornecedorRepositorio.ObterPorId(Id);

            if (fornecedor == null) return NotFound();

            await _fornecedorAppServico.Remover(fornecedor);

            return CustomResponse();
        }

        public async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid Id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepositorio.ObterFornecedorProdutosEndereco(Id));
        }

        public async Task<ActionResult<FornecedorViewModel>> ObterFornecedorEndereco(Guid Id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepositorio.ObterFornecedorEndereco(Id));
        }
    }
}
