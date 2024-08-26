using ControleFinanceiro.Application.Transacao;
using ControleFinanceiro.Application.Transacao.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacaoController : ControllerBase
    {

        private readonly ILogger<TransacaoController> _logger;
        private TransacaoService _transacaoService;

        public TransacaoController(ILogger<TransacaoController> logger, TransacaoService transacaoService)
        {
            _logger = logger;
            _transacaoService = transacaoService;
        }

        [HttpGet]
        [Route("ObterTransacaoPorId")]
        public IActionResult ObterTransacoesPorId(Guid IdConta)
        {
            var result = _transacaoService.ListarTransacoes(IdConta);
            
            return Ok(result);
        }


        [HttpPost]
        [Route("InserirTransacao")]
        public IActionResult InserirTransacao(TransacaoRequestDto transacao)
        {
            var result = _transacaoService.InserirTransacao(transacao, transacao.IdConta);

            return Ok(result);

        }

        [HttpPut]
        [Route("AlterarTransacao")]
        public IActionResult AlterarTransacao(TransacaoDto transacao)
        {
            _transacaoService.EditarTransacao(transacao);

            return Ok();
        }

        [HttpDelete]
        [Route("DeleteTransacao")]
        public IActionResult DeleteTransacao(Guid IdTransacao)
        {
            var result = _transacaoService.ExcluirTransacao(IdTransacao);
            return Ok(result);
        }
    }
}
