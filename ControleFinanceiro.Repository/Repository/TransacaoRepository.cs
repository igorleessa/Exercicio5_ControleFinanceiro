using ControleFinanceiro.Domain.Conta.Agreggates;
using ControleFinanceiro.Domain.Transacao.Agreggates;
using ControleFinanceiro.Repository.Configuration;
using System.Data.SqlClient;

namespace ControleFinanceiro.Repository.Repository
{
    public class TransacaoRepository : SqlConfigurator
    {
        Transacao _transacao = new Transacao();

        public Transacao InserirTransacao(Transacao transacaoObj)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand($@"INSERT INTO Transacao
	                                        (IdConta, Tipo, Valor, Descricao, DataMovimentacao)
                                        OUTPUT INSERTED.Id AS IdTransacao, INSERTED.IdConta, INSERTED.Tipo, INSERTED.Valor, INSERTED.Descricao, INSERTED.DataMovimentacao
                                        VALUES
                                            ('{transacaoObj.Conta.Id}', {transacaoObj.Tipo}, {transacaoObj.Valor}, '{transacaoObj.Descricao}', '{transacaoObj.DataMovimentacao}')", Con);
                Dr = Cmd.ExecuteReader();

                if (Dr.Read())
                {
                    _transacao = new Transacao();
                    PreencheTransacao();
                }

                return _transacao;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void EditarTransacao(Transacao transacaoObj)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand($@"UPDATE Transacao SET 
	                                        Tipo = {transacaoObj.Tipo}, Valor = {transacaoObj.Valor}, Descricao = '{transacaoObj.Descricao}', DataMovimentacao = '{transacaoObj.DataMovimentacao}'
                                        WHERE 
	                                        Id = '{transacaoObj.Id}'", Con);
                Dr = Cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<Transacao> ListarTransacoesPorConta(Guid Id)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand($@"SELECT Id as IdTransacao, IdConta, Tipo, Valor, Descricao, DataMovimentacao FROM Transacao
                                            WHERE 
                                        IdConta = '{Id}'", Con);
                Dr = Cmd.ExecuteReader();

                var list = new List<Transacao>();
                while (Dr.Read())
                {
                    _transacao = new Transacao();
                    PreencheTransacao();

                    list.Add(_transacao);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool ExcluirTransacao(Guid Id)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand($@"DELETE FROM Transacao 
	                                       WHERE Id = '{Id}'", Con);
                Dr = Cmd.ExecuteReader();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public Guid ObterIdContaDaTransacao(Guid IdTransacao)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand($@"SELECT IdConta FROM Transacao WHERE Id = '{IdTransacao}'", Con);
                
                Dr = Cmd.ExecuteReader();

                var list = new List<Transacao>();
                if (Dr.Read())
                {
                    return (Guid)Dr["IdConta"];
                }
                else
                    throw new ArgumentNullException("Não existe Conta associada ao transação.");
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        private void PreencheTransacao()
        {
            _transacao.Id = (Guid)Dr["IdTransacao"];
            _transacao.Conta = new Conta();
            _transacao.Conta.Id = (Guid)Dr["IdConta"];
            _transacao.Tipo = (Int32)Dr["Tipo"];
            _transacao.Valor = (decimal)Dr["Valor"];
            _transacao.Descricao = (string)Dr["Descricao"];
            _transacao.DataMovimentacao = (DateTime)Dr["DataMovimentacao"];
        }
    }
}
