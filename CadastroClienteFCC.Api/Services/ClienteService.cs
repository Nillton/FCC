using CadastroClienteFCC.Api.DAO;
using CadastroClienteFCC.Api.Model;
using CadastroClienteFCC.Api.Services.Interface;
using System.Data;

namespace CadastroClienteFCC.Api.Services
{
    public class ClienteService : IClienteService
    {
        GeralDAO _geralDAO = new GeralDAO();
        ClienteDAO _clienteDAO = new ClienteDAO();

        public async Task<List<ClienteModel>> GetClienteAsync()
        {
            using (var conn = await _geralDAO.OpenConnectionAsync())
            {
                var queryResultList = await _clienteDAO.ListaConsultaClientesAsync(conn);
                await _geralDAO.CloseConnection(conn);
                return queryResultList;
            }            
        }

        public async Task<ClienteModel> GetIdClienteAsync(int idCliente)
        {
            using (var conn = await _geralDAO.OpenConnectionAsync())
            {
                var queryResult = await _clienteDAO.ListaClienteIdAsync(conn, idCliente);
                await _geralDAO.CloseConnection(conn);
                return queryResult;
            }
            
        }

        public async Task<bool> PostClienteAsync(ClienteModel clienteModel)
        {
            using (var conn = await _geralDAO.OpenConnectionAsync())
            {
                var result = await _clienteDAO.InserirCliente(conn, clienteModel);
                await _geralDAO.CloseConnection(conn);
                if (result)
                {
                    return true;
                }
                else
                {
                    return false;
                } 
            }
        }

        public async Task<bool> PutClienteAsync(ClienteModel clienteModel)
        {
            using (var conn = await _geralDAO.OpenConnectionAsync())
            {
                var result = await _clienteDAO.AtualizarCliente(conn, clienteModel);
                await _geralDAO.CloseConnection(conn);
                return result;
            }
        }

        public async Task<bool> DeleteClienteAsync(int idCliente)
        {
            using (var conn = await _geralDAO.OpenConnectionAsync())
            {
                var result = await _clienteDAO.DeletarCliente(conn, idCliente);
                await _geralDAO.CloseConnection(conn);
                return result;
            }
        }
    }
}
