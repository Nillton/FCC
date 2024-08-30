using CadastroClienteFCC.Api.Model;

namespace CadastroClienteFCC.Api.Services.Interface
{
    public interface IClienteService
    {
        Task<List<ClienteModel>> GetClienteAsync();
        Task<ClienteModel> GetIdClienteAsync(int idCliente);
        Task<bool> PostClienteAsync(ClienteModel clienteModel);
        Task<bool> PutClienteAsync(ClienteModel clienteModel);
        Task<bool> DeleteClienteAsync(int idCliente);
    }
}
