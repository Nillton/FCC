using CadastroClienteFCC.Api.Model;

namespace CadastroClienteFCC.Api.Services.Interface
{
    public interface IJwtService
    {
        LoginResponseModel Authenticate(string userName, string password);
    }
}
