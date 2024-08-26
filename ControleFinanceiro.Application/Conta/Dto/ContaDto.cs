using ControleFinanceiro.Application.Core;
using ControleFinanceiro.Application.Transacao.Dto;

namespace ControleFinanceiro.Application.Conta.Dto
{
    public class ContaDto 
    {
        public Guid Id { get; set; }
        public decimal Saldo { get; set; }
        public virtual List<TransacaoDto> Transacoes { get; set; }
    }
}
