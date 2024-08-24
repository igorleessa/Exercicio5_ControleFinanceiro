namespace ControleFinanceiro.Domain.Conta.Agreggates
{
    public class Conta : Usuario
    {
        public Guid Id { get; set; }
        public Usuario Usuario { get; set; }
        public decimal Saldo { get; set; }
        public virtual List<Transacao.Agreggates.Transacao> Transacoes{ get; set; }
    }
}
