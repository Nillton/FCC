using System.Data.SqlClient;
using System.Data;

namespace CadastroClienteFCC.Api.DAO
{
    public class GeralDAO
    {
        protected string connectionString = "Server=(LocalDB)\\MSSQLLocalDB;Database=BD_CadastroClienteFCC;User Id=sa;Password=123456;";

        public async Task<SqlConnection> OpenConnectionAsync()
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {  
                await con.OpenAsync();  
                return con;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"OpenConnectionAsync - Error: {ex}");
                throw;
            }
        }

        public async Task CloseConnection(SqlConnection con)
        {
            try
            {
                if (con != null)
                    if (con.State == ConnectionState.Open)
                        await con.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CloseConnectionAsync - Error: {ex}");
                throw;
            }
        }
    }
}
