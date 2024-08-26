using ControleFinanceiro.Application.Core;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Application.Transacao.Dto
{
    public class TransacaoDto 
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public int Tipo { get; set; }
        
        [Required]
        public decimal Valor { get; set; }
        
        [Required]
        public string Descricao { get; set; }
        
        public DateTime DataMovimentacao { get; set; }
    }
}
