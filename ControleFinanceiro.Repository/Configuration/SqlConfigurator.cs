using System.Configuration;
using System.Data.SqlClient;

namespace ControleFinanceiro.Repository.Configuration
{
    public class SqlConfigurator
    {
        protected SqlConnection Con;
        protected SqlCommand Cmd;
        protected SqlDataReader Dr;

        protected void OpenConnection()
        {
            Con = new SqlConnection(ConfigurationManager.ConnectionStrings[""].ConnectionString);

            Con.Open();
        }
        protected void CloseConnection()
        {
            Con.Close();
        }
    }
}
