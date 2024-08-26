using ControleFinanceiro.Domain.Conta.Agreggates;
using ControleFinanceiro.Repository.Configuration;
using System.Data.SqlClient;


namespace ControleFinanceiro.Repository.Repository
{
    public class ContaRepository : SqlConfigurator
    {
        Conta _conta = new Conta();

        public Conta InserirConta(Guid IdUsuario)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand($@"INSERT INTO Conta
                                            (IdUsuario)
                                        OUTPUT INSERTED.Id as IdConta, INSERTED.IdUsuario, INSERTED.Saldo
                                        VALUES
                                            ('{IdUsuario}')", Con);
                Dr = Cmd.ExecuteReader();


                if (Dr.Read())
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
                                        	SET Saldo = @Saldo
                                        WHERE 
                                        	Id = '{contaObj.Id}'", Con);

                Cmd.Parameters.AddWithValue("@Saldo", contaObj.Saldo);
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

        public Conta ObterContasUsuario(Guid Id)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand($@"SELECT Id AS IdConta, IdUsuario, Saldo FROM Conta
                                        WHERE 
                                        	IdUsuario = '{Id}'", Con);
                Dr = Cmd.ExecuteReader();

                if (Dr.Read())
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


        public void PreencheConta()
        {
            _conta.Id = (Guid)Dr["IdConta"];
            _conta.Saldo = (decimal)Dr["Saldo"];
			_conta.Transacoes = new List<Domain.Transacao.Agreggates.Transacao>();
        }

    }
}
