namespace ControleFinanceiro.Domain.Transacao.Agreggates
{
    public class Transacao
    {
        public Guid Id { get; set; }
        public virtual Conta.Agreggates.Conta Conta { get; set; }
        public int Tipo { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public DateTime DataMovimentacao { get; set; }
    }
}
