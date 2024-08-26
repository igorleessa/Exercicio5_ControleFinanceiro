using ControleFinanceiro.Application.Conta;
using ControleFinanceiro.Application.Conta.Dto;

namespace ControleFinanceiro.Test
{
    public class UsuarioTest
    {
        private UsuarioService _service;

        public UsuarioTest(UsuarioService service)
        {
            _service = service;
        }

        [Fact]
        public void CriarUsuarioSucesso()
        {
            var request = new UsuarioRequestDto
            {
                Nome = "Teste",
                Email = "teste@gmail.com",
                Telefone = "2299999-9999",
                FlAtivo = true
            };

            var usuario = _service.Inserir(request);
            Assert.True(usuario != null);
        }

        [Fact]
        public void CriarUsuarioFalha()
        {
            var request = new UsuarioRequestDto();

            var usuario = _service.Inserir(request);
            Assert.True(usuario == null);
        }
    }
}