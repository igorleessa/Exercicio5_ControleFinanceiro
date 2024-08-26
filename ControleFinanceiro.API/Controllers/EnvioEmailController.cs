using ControleFinanceiro.Application.EnvioEmail;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnvioEmailController : ControllerBase
    {

        private readonly ILogger<EnvioEmailController> _logger;
        private EnvioEmailService _envioEmailService;

        public EnvioEmailController(ILogger<EnvioEmailController> logger, EnvioEmailService envioEmailService)
        {
            _logger = logger;
            _envioEmailService = envioEmailService;
        }

        [HttpGet]
        [Route("CriarRotina")]
        public async Task ListarUsuarios()
        {
            _envioEmailService.EnvioEmailSchedulerTask();
        }

    }
}
