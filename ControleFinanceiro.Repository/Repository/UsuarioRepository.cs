using ControleFinanceiro.Domain.Conta.Agreggates;
using ControleFinanceiro.Repository.Configuration;
using System.Data.SqlClient;


namespace ControleFinanceiro.Repository.Repository
{
    public class UsuarioRepository : SqlConfigurator
    {
        Usuario _usuario = new Usuario();

        public List<Usuario> ObterUsuarios()
        {
			try
			{
				OpenConnection();
				Cmd = new SqlCommand($@"SELECT 
											us.Id as IdUsuario, us.Nome, us.Email, us.Telefone, us.FlAtivo
											, co.Id as IdConta, co.Saldo
										FROM Usuario us
											LEFT JOIN Conta co 
												ON us.Id = co.IdUsuario
                                        WHERE us.FlAtivo = 1", Con);
				Dr = Cmd.ExecuteReader();

				var list = new List<Usuario>();
				
                while (Dr.Read())
				{
					_usuario = new Usuario();
                    PreencheUsuario();
                    _usuario.Conta = new Conta();
                    PreencheConta();

                    list.Add(_usuario);
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


        public Usuario ObterUsuarioById(Guid Id)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand($@"SELECT 
											us.Id as IdUsuario, us.Nome, us.Email, us.Telefone, us.FlAtivo
											, co.Id as IdConta, co.Saldo
										FROM Usuario us
											LEFT JOIN Conta co 
												ON us.Id = co.IdUsuario
                                        WHERE 
	                                        us.FlAtivo = 1 AND us.Id = '{Id}'", Con);
                Dr = Cmd.ExecuteReader();

                if (Dr.Read())
                {
                    _usuario = new Usuario();
                    PreencheUsuario();
                    _usuario.Conta = new Conta();
                    PreencheConta();
                }

                return _usuario;
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

        public Usuario InserirUsuario(Usuario usuarioObj)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand($@"INSERT INTO Usuario 
                                        	(Nome, Email, Telefone, FlAtivo)
                                        OUTPUT INSERTED.Id AS IdUsuario, INSERTED.Nome, INSERTED.Email, INSERTED.Telefone, INSERTED.FlAtivo
                                        VALUES
                                        	('{usuarioObj.Nome}', '{usuarioObj.Email}', '{usuarioObj.Telefone}', 1)", Con);
                Dr = Cmd.ExecuteReader();


                if (Dr.Read())
                {
                    _usuario = new Usuario();
                    PreencheUsuario();
                    _usuario.Conta = new Conta();
                    _usuario.Conta.Transacoes = new List<Domain.Transacao.Agreggates.Transacao>();
                }

                return _usuario;
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

        public void EditarUsuario(Usuario usuarioObj)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand($@"UPDATE Usuario SET
                                        	Nome = '{usuarioObj.Nome}', Email = '{usuarioObj.Email}', Telefone = '{usuarioObj.Telefone}'
                                        WHERE
                                        	Id = '{usuarioObj.Id}'", Con);
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

        public void ExcluirUsuario(Guid Id)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand($@"UPDATE Usuario SET
                                        	FlAtivo = 0
                                        WHERE
                                        	Id = '{Id}'", Con);
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

        public void PreencheUsuario()
        {
            _usuario.Id = (Guid)Dr["IdUsuario"];
            _usuario.Nome = (string)Dr["Nome"];
            _usuario.Telefone = (string)Dr["Telefone"];
            _usuario.Email = (string)Dr["Email"];
            _usuario.FlAtivo = (bool)Dr["FlAtivo"];
        }

        public void PreencheConta()
        {
            _usuario.Conta.Id = (Guid)Dr["IdConta"];
            _usuario.Conta.Saldo = (decimal)Dr["Saldo"];
			_usuario.Conta.Transacoes = new List<Domain.Transacao.Agreggates.Transacao>();
        }

    }
}
