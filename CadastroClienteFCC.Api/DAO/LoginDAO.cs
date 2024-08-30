using CadastroClienteFCC.Api.Model;
using System.Data.SqlClient;

namespace CadastroClienteFCC.Api.DAO
{
    public class LoginDAO
    {
        public async Task<bool> ValidaLogin(SqlConnection connection, LoginModel login)
        {
            var queryString = @"SELECT COUNT(*)
                                FROM Usuario
                                WHERE UserName = @UserName AND Password = @Password;";

            using (var command = new SqlCommand(queryString, connection))
            {                
                command.Parameters.AddWithValue("@UserName", login.UserName);
                command.Parameters.AddWithValue("@Password", login.Password);

                int userCount = (int)await command.ExecuteScalarAsync();
                return userCount > 0;
            }
        }
    }
}
