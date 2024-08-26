using ControleFinanceiro.Application.Conta.Dto;
using ControleFinanceiro.Application.Transacao.Dto;
using ControleFinanceiro.Application.Transacao.Enums;
using ControleFinanceiro.Application.Transacao.Validator;
using ControleFinanceiro.Repository.Repository;

namespace ControleFinanceiro.Application.Transacao
{
    public class TransacaoService
    {

        private TransacaoRepository _transacaoRepository { get; set; }
        private ContaRepository _contaRepository { get; set; }
        private TransacaoValidator _IsValidTransacao = new TransacaoValidator();

        public TransacaoService(TransacaoRepository transacaoRepository, ContaRepository contaRepository)
        {
            _transacaoRepository = transacaoRepository;
            _contaRepository = contaRepository;
        }

        public TransacaoDto InserirTransacao(TransacaoRequestDto transacao, Guid IdConta)
        {
            if (transacao == null)
                throw new ArgumentNullException("Transação não pode ser nula");

            var mensagemErro = _IsValidTransacao.ValidaTransacaoRequest(transacao);
            if(!string.IsNullOrWhiteSpace(mensagemErro))
                throw new ArgumentNullException(mensagemErro);

            var transacaoObj = new Domain.Transacao.Agreggates.Transacao
            {
                Conta = new Domain.Conta.Agreggates.Conta
                {
                    Id = IdConta
                },
                Tipo = transacao.Tipo,
                Valor = transacao.Valor,
                Descricao = transacao.Descricao,
                DataMovimentacao = transacao.DataMovimentacao
            };

            var transacaoInserida = _transacaoRepository.InserirTransacao(transacaoObj);

            var conta = new ContaDto { Id = IdConta };

            NormalizarSaldo(conta);

            return new TransacaoDto
            {
                Id = transacaoInserida.Id,
                Tipo = transacaoInserida.Tipo,
                Valor = transacaoInserida.Valor,
                Descricao = transacaoInserida.Descricao,
                DataMovimentacao = transacaoInserida.DataMovimentacao
            };
        }

        public void EditarTransacao(TransacaoDto transacao)
        {
            

            var transacaoObj = new Domain.Transacao.Agreggates.Transacao
            {
                Id = transacao.Id,
                Tipo = transacao.Tipo,
                Valor = transacao.Valor,
                Descricao = transacao.Descricao,
                DataMovimentacao = transacao.DataMovimentacao
            };

            _transacaoRepository.EditarTransacao(transacaoObj);
            var idConta = _transacaoRepository.ObterIdContaDaTransacao(transacao.Id);

            var conta = new ContaDto
            {
                Id = idConta
            };

            NormalizarSaldo(conta);
        }

        public bool ExcluirTransacao(Guid IdTransacao)
        {
            var idConta = _transacaoRepository.ObterIdContaDaTransacao(IdTransacao);

            var conta = new ContaDto
            {
                Id = idConta
            };

            _transacaoRepository.ExcluirTransacao(IdTransacao);

            NormalizarSaldo(conta);
            return true;
        }

        public void NormalizarSaldo(ContaDto conta)
        {
            var transacoes = _transacaoRepository.ListarTransacoesPorConta(conta.Id);

            decimal total = 0;

            foreach (var item in transacoes)
            {
                if (item.Tipo.Equals((int)TipoTransacaoEnum.Despesa))
                    total -= item.Valor;
                else
                    total += item.Valor;
            }

            conta.Saldo = total;

            var obj = new Domain.Conta.Agreggates.Conta
            {
                Id = conta.Id,
                Saldo = conta.Saldo
            };

            _contaRepository.AlterarSaldo(obj);
        }

        public List<TransacaoDto> ListarTransacoes(Guid Id)
        {
            var list = _transacaoRepository.ListarTransacoesPorConta(Id);
            var transacoes = list.Select(t => new TransacaoDto
            {
                Id = t.Id,
                Valor = t.Valor,
                Tipo = t.Tipo,
                Descricao = t.Descricao,
                DataMovimentacao = t.DataMovimentacao
            }).ToList();
            return transacoes;
        }
    }
}
