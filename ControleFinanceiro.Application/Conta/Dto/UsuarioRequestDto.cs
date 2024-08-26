using ControleFinanceiro.Application.Core;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Application.Conta.Dto
{
    public class UsuarioRequestDto 
    {
        //public Guid Id { get; set; }
        
        [Required]
        public string Nome { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Telefone { get; set; }
        
        [Required]
        public Boolean FlAtivo { get; set; }
        
    }
}
