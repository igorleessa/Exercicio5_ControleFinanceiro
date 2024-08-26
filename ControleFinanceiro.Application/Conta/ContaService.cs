using ControleFinanceiro.Application.Conta.Dto;
using ControleFinanceiro.Application.Transacao.Dto;
using ControleFinanceiro.Application.Transacao.Enums;
using ControleFinanceiro.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Application.Conta
{
    public class ContaService
    {
        private ContaRepository _contaRepository { get; set; }
        private TransacaoRepository _transacaoRepository { get; set; }

        public ContaService(ContaRepository contaRepository, TransacaoRepository transacaoRepository)
        {
            _contaRepository = contaRepository;
            _transacaoRepository = transacaoRepository;
        }

        //public ContaDto InserirConta(ContaDto conta)
        //{
        //    var contaObj = new Domain.Conta.Agreggates.Conta
        //    {
        //        Id = conta.Id,
        //        Saldo = conta.Saldo
        //    };

        //    var contaInserida = _contaRepository.InserirConta(contaObj);

        //    return new ContaDto
        //    {
        //        Id = contaInserida.Id,
        //        Saldo = contaInserida.Saldo
        //    };
        //}

        public ContaDto ObterContaPorId(Guid Id)
        {
            var contaObj = _contaRepository.ObterContasUsuario(Id);
            var conta = new ContaDto { 
                Id = contaObj.Id,
                Saldo = contaObj.Saldo,
                Transacoes = new List<TransacaoDto>()
            };

            var listTransacoes = _transacaoRepository.ListarTransacoesPorConta(conta.Id);
            conta.Transacoes = listTransacoes.Select(t => new TransacaoDto
            {
                Id = t.Id,
                Valor = t.Valor,
                Tipo = t.Tipo,
                Descricao = t.Descricao,
                DataMovimentacao = t.DataMovimentacao
            }).ToList();

            return conta;
        }

        
    }
}
