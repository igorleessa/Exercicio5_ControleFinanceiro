using ControleFinanceiro.Domain.Conta.Agreggates;
using ControleFinanceiro.Repository.Configuration;
using System.Data.SqlClient;


namespace ControleFinanceiro.Repository.Repository
{
    public class ContaRepository : SqlConfigurator
    {
        Conta _conta = new Conta();

        public Conta InserirConta(Conta contaObj)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand($@"INSERT INTO Conta
                                            (IdUsuario, Saldo)
                                        VALUES
                                            ('{contaObj.Id}', {contaObj.Saldo})
                                        OUTPUT INSERTED.Id VALUES (Id, IdUsuario, Saldo)", Con);
                Dr = Cmd.ExecuteReader();


                if (Dr.HasRows)
                {
                    _conta = new Conta();
                    PreencheConta();
                }

                return _conta;
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

        public void AlterarSaldo(Conta contaObj)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand($@"UPDATE Conta
                                        	SET SALDO = {contaObj.Saldo}
                                        WHERE 
                                        	Id = '{contaObj.Id}'", Con);
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


        public void PreencheConta()
        {
            _conta.Conta.Id = new Guid((string)Dr["IdConta"]);
            _conta.Conta.Saldo = (decimal)Dr["Saldo"];
			_conta.Conta.Transacoes = new List<Domain.Transacao.Agreggates.Transacao>();
        }

    }
}
