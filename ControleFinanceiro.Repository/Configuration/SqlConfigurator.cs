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
            //Con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            Con = new SqlConnection("Data Source=DESKTOP-VKDVPGR;Initial Catalog=ControleFinanceiro;Integrated Security=True;");
            Con.Open();
        }
        protected void CloseConnection()
        {
            Con.Close();
        }
    }
}
