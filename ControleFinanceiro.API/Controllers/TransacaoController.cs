using ControleFinanceiro.Application.Transacao;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacaoController : ControllerBase
    {

        private readonly ILogger<TransacaoController> _logger;

        public TransacaoController(ILogger<TransacaoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("ObterTransacaoPorId")]
        public TransacaoDto ObterTransacaoPorId(Guid Id)
        {
            return new TransacaoDto();

        }

        [HttpGet]
        [Route("ObterExtratoFinanceiro")]
        public TransacaoDto ObterExtratoFinanceiro(Guid IdConta)
        {
            return new TransacaoDto();

        }

        [HttpPost]
        [Route("InserirTransacao")]
        public TransacaoDto InserirTransacao(TransacaoDto transacao)
        {
            return new TransacaoDto();

        }

        [HttpPut]
        [Route("AlterarTransacao")]
        public TransacaoDto AlterarTransacao(TransacaoDto transacao)
        {
            return new TransacaoDto();

        }

        [HttpDelete]
        [Route("DeleteTransacao")]
        public bool DeleteTransacao(TransacaoDto transacao)
        {
            return true;

        }
    }
}
