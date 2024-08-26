using ControleFinanceiro.Application.Core;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Application.Transacao.Dto
{
    public class TransacaoRequestDto 
    {
        [Required]
        public Guid IdConta { get; set; }
        
        [Required]
        public int Tipo { get; set; }
        
        [Required]
        public decimal Valor { get; set; }
        
        [Required]
        public string Descricao { get; set; }
        
        public DateTime DataMovimentacao { get; set; }
    }
}
