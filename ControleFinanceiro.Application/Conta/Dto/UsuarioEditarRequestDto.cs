using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Application.Conta.Dto
{
    public class UsuarioEditarRequestDto : UsuarioRequestDto
    {
        [Required]
        public Guid Id { get; set; }
        
    }
}
