using ControleFinanceiro.Application.Conta.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("ListarUsuarios")]
        public IEnumerable<UsuarioDto> ListarUsuarios()
        {
            return new List<UsuarioDto>();
            
        }

        [HttpGet]
        [Route("ListarUsuarioPorId")]
        public UsuarioDto ListarUsuarioPorId()
        {
            return new UsuarioDto();

        }

        [HttpPost]
        [Route("InserirUsuario")]
        public UsuarioDto InserirUsuario(UsuarioDto usuario)
        {
            return new UsuarioDto();

        }

        [HttpPut]
        [Route("EditarUsuario")]
        public UsuarioDto EditarUsuario(UsuarioDto usuario)
        {
            return new UsuarioDto();

        }

        [HttpDelete]
        [Route("DeleteUsuario")]
        public bool DeleteUsuario(UsuarioDto usuario)
        {
            return true;

        }
    }
}
