using ControleFinanceiro.Application.Conta.Dto;
using System.Text.RegularExpressions;

namespace ControleFinanceiro.Application.Conta.Validator
{
    public class UsuarioValidator
    {
        public string ValidaUsuarioRequest(UsuarioRequestDto request)
        {
            var message = string.Empty;

            if (string.IsNullOrWhiteSpace(request.Nome))
                message += "Nome é obrigatório\n";

            if (string.IsNullOrWhiteSpace(request.Email))
                message += "Email é obrigatório\n";
            else if(!ValidaEmail(request.Email))
                message += "Email inválido\n";

            if (string.IsNullOrWhiteSpace(request.Telefone))
                message += "Telefone é obrigatório\n";
            else if (!ValidaTelefone(request.Telefone))
                message += "Telefone inválido\n";

            return message;
        }


        public string ValidaUsuarioEditarRequest(UsuarioEditarRequestDto request)
        {
            var message = string.Empty;

            if (string.IsNullOrWhiteSpace(request.Nome))
                message += "Nome é obrigatório\n";

            if (string.IsNullOrWhiteSpace(request.Email))
                message += "Email é obrigatório\n";
            else if (!ValidaEmail(request.Email))
                message += "Email inválido\n";

            if (string.IsNullOrWhiteSpace(request.Telefone))
                message += "Telefone é obrigatório\n";
            else if (!ValidaTelefone(request.Telefone))
                message += "Telefone inválido\n";

            return message;
        }
        public bool ValidaEmail(string email)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }

        public bool ValidaTelefone(string telefone)
        {
            var regex = new Regex(@"^\([1-9]{2}\) [2-9][0-9]{3,4}\-[0-9]{4}$");
            Match match = regex.Match(telefone);
            return match.Success;
        }
    }
}
