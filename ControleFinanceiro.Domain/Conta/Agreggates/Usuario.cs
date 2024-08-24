namespace ControleFinanceiro.Domain.Conta.Agreggates
{
    public class Usuario
    {
        public Guid Id { get; set; } 
        public string Nome { get; set; } 
        public string Email { get; set; } 
        public string Telefone { get; set; }
        public Boolean FlAtivo { get; set; }
        public Conta Conta { get; set; }
    }
}
