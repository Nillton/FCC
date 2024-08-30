using CadastroClienteFCC.Api.Model;

namespace CadastroClienteFCC.Api.Services.Interface
{
    public interface ILoginService
    {
        Task<bool> GetLoginUsuario(LoginModel login);
    }
}
