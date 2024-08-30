using CadastroClienteFCC.Api.DAO;
using CadastroClienteFCC.Api.Model;
using CadastroClienteFCC.Api.Services.Interface;

namespace CadastroClienteFCC.Api.Services
{
    public class LoginService : ILoginService
    {
        GeralDAO _geralDAO = new GeralDAO();
        LoginDAO _loginDAO = new LoginDAO();        

        public async Task<bool> GetLoginUsuario(LoginModel login)
        {
            using (var conn = await _geralDAO.OpenConnectionAsync())
            {
                var resultValidaLogin = await _loginDAO.ValidaLogin(conn, login);
                await _geralDAO.CloseConnection(conn);   
                return resultValidaLogin;
            }
        }
    }
}
