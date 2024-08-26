using ControleFinanceiro.Application.Conta;
using ControleFinanceiro.Application.Conta.Dto;
using ControleFinanceiro.Application.Transacao;
using ControleFinanceiro.Application.Transacao.Dto;

namespace ControleFinanceiro.Test
{
    public class TransacaoTest
    {
        private TransacaoService _service;

        public TransacaoTest(TransacaoService service)
        {
            _service = service;
        }

        [Fact]
        public void CriarTransacaoSucesso()
        {
            var request = new TransacaoRequestDto
            { 
                IdConta = new Guid("9BFB2454-5B12-4E80-AB4F-BA0C56AC5E35"),
                Tipo = 1, 
                Valor = 100, 
                Descricao = "Teste", 
                DataMovimentacao = DateTime.Now 
            };

            var transacao = _service.InserirTransacao(request, request.IdConta);
            Assert.True(transacao != null);
        }

        [Fact]
        public void CriarTransacaoFalha()
        {
            var request = new TransacaoRequestDto();

            var transacao = _service.InserirTransacao(request, request.IdConta);
            Assert.True(transacao == null);
        }
    }
}