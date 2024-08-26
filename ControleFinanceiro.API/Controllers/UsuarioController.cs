using ControleFinanceiro.Application.Conta;
using ControleFinanceiro.Application.Conta.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {

        private readonly ILogger<UsuarioController> _logger;
        private UsuarioService _usuarioService;

        public UsuarioController(ILogger<UsuarioController> logger, UsuarioService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("ListarUsuarios")]
        public IActionResult ListarUsuarios()
        {
            var result = _usuarioService.Listar();

            if (result == null)
                return NotFound();
            
            return Ok(result);
        }

        [HttpGet]
        [Route("ObterUsuarioPorId")]
        public IActionResult ObterUsuarioPorId(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _usuarioService.Obter(id);
            
            if (result == null)
                return NotFound();

            return Ok(result);

        }

        [HttpPost]
        [Route("InserirUsuario")]
        public IActionResult InserirUsuario(UsuarioRequestDto usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _usuarioService.Inserir(usuario);
            
            if (result == null)
                return NotFound();

            return Ok(result);

        }

        [HttpPut]
        [Route("EditarUsuario")]
        public IActionResult EditarUsuario(UsuarioEditarRequestDto usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_usuarioService.Editar(usuario));
        }

        [HttpDelete]
        [Route("DeleteUsuario")]
        public IActionResult DeleteUsuario(Guid Id)
        {
            var result = _usuarioService.Excluir(Id);
            var response = new
            {
                message = result ? "Usuário excluído com sucesso" : "Erro ao excluir usuário"
            };
            return Ok(response);

        }
    }
}
