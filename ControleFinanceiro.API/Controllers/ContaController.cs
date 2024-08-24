using ControleFinanceiro.Application.Conta.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaController : ControllerBase
    {

        private readonly ILogger<ContaController> _logger;

        public ContaController(ILogger<ContaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("ObterContaPorId")]
        public ContaDto ObterContaPorId(Guid Id)
        {
            return new ContaDto();

        }

        [HttpPost]
        [Route("InserirConta")]
        public ContaDto InserirConta(ContaDto conta)
        {
            return new ContaDto();

        }

        [HttpPut]
        [Route("AlterarSaldo")]
        public ContaDto AlterarSaldo(ContaDto conta)
        {
            return new ContaDto();

        }

        [HttpDelete]
        [Route("DeleteConta")]
        public bool DeleteConta(ContaDto conta)
        {
            return true;

        }
    }
}
