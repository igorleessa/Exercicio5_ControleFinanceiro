using ControleFinanceiro.Application.Transacao.Dto;

namespace ControleFinanceiro.Application.Transacao.Validator
{
    public class TransacaoValidator
    {
        public string ValidaTransacao(TransacaoDto request)
        {
            var message = string.Empty;
            if (request.Tipo != 1 || request.Tipo != 2)
                message += "O tipo deve ser 1 (Receita) ou 2 (Despesa)";

            if (request.Valor == null)
                message += "Valor não pode ser nulo";

            if (request.Descricao == null)
                message += "Descrição não pode ser nula";

            return message;
        }

        public string ValidaTransacaoRequest(TransacaoRequestDto request)
        {
            var message = string.Empty;
            if (request.Tipo != 1 || request.Tipo != 2)
                message += "O tipo deve ser 1 (Receita) ou 2 (Despesa)";

            if (request.Valor == null)
                message += "Valor não pode ser nulo";

            if (request.Descricao == null)
                message += "Descrição não pode ser nula";

            return message;
        }
    }
}
