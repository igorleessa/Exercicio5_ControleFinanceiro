using ControleFinanceiro.Application.Conta;
using ControleFinanceiro.Application.Conta.Dto;
using ControleFinanceiro.Repository.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaController : ControllerBase
    {

        private readonly ILogger<ContaController> _logger;
        private ContaService _contaService;

        public ContaController(ILogger<ContaController> logger, ContaService contaService)
        {
            _logger = logger;
            _contaService = contaService;
        }

        [HttpGet]
        [Route("ObterContaPorId")]
        public IActionResult ObterContaPorId(Guid IdUsuario)
        {
            var result = _contaService.ObterContaPorId(IdUsuario);
            return Ok(result);
        }

    }
}
