using CadastroClienteFCC.Api.Model;
using System.Data.SqlClient;

namespace CadastroClienteFCC.Api.DAO
{
    public class ClienteDAO
    {
        public async Task<List<ClienteModel>> ListaConsultaClientesAsync(SqlConnection connection)
        {
            List<ClienteModel> listaCliente = new List<ClienteModel>();
            var queryString =
                $@"SELECT IdCliente
                         ,CPF
                         ,Nome
                         ,RG
                         ,DataExpedicaoRG
                         ,OrgaoExpedidor
                         ,UF
                         ,DataNascimento
                         ,Sexo
                         ,EstadoCivil
                  FROM Clientes";

            using (var command = new SqlCommand(queryString, connection))
            {
                var reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ClienteModel cliente = DadosClienteModel(reader);
                        listaCliente.Add(cliente);
                    }
                    var includeList = listaCliente.Count();
                }
            }
            return listaCliente;
        }

        public async Task<ClienteModel> ListaClienteIdAsync(SqlConnection connection, int id)
        {
            ClienteModel cliente = new ClienteModel();
            var queryString =
                $@"SELECT IdCliente
                         ,CPF
                         ,Nome
                         ,RG
                         ,DataExpedicaoRG
                         ,OrgaoExpedidor
                         ,UF
                         ,DataNascimento
                         ,Sexo
                         ,EstadoCivil
                  FROM Clientes
                  WHERE IdCliente = {id}";

            using (var command = new SqlCommand(queryString, connection))
            {
                var reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cliente = DadosClienteModel(reader);                        
                    }                    
                }
            }
            return cliente;
        }

        public async Task<bool> InserirCliente(SqlConnection connection, ClienteModel cliente)
        {
            var queryString = @"INSERT INTO Clientes 
                                (CPF, Nome, RG, DataExpedicaoRG, OrgaoExpedidor, UF, DataNascimento, Sexo, EstadoCivil)
                                VALUES 
                                (@CPF, @Nome, @RG, @DataExpedicaoRG, @OrgaoExpedidor, @UF, @DataNascimento, @Sexo, @EstadoCivil);";

            using (var command = new SqlCommand(queryString, connection))
            {
                DadosCliente(cliente, command);
                int linhaInserida = await command.ExecuteNonQueryAsync();
                return linhaInserida > 0;
            }
        }        

        public async Task<bool> AtualizarCliente(SqlConnection connection, ClienteModel cliente)
        {
            var queryString = @"UPDATE Clientes 
                                SET CPF = @CPF, 
                                    Nome = @Nome, 
                                    RG = @RG, 
                                    DataExpedicaoRG = @DataExpedicaoRG, 
                                    OrgaoExpedidor = @OrgaoExpedidor, 
                                    UF = @UF, 
                                    DataNascimento = @DataNascimento, 
                                    Sexo = @Sexo, 
                                    EstadoCivil = @EstadoCivil
                                WHERE IdCliente = @IdCliente;";

            using (var command = new SqlCommand(queryString, connection))
            {
                DadosCliente(cliente, command);
                int rowsAffected = await command.ExecuteNonQueryAsync();                
                return rowsAffected > 0;
            }
        }

        public async Task<bool> DeletarCliente(SqlConnection connection, int idCliente)
        {
            var queryString = @"DELETE FROM Clientes 
                                WHERE IdCliente = @IdCliente;";

            using (var command = new SqlCommand(queryString, connection))
            {                
                command.Parameters.AddWithValue("@IdCliente", idCliente);                
                int rowsAffected = await command.ExecuteNonQueryAsync();                
                return rowsAffected > 0;
            }
        }

        private static void DadosCliente(ClienteModel cliente, SqlCommand command)
        {
            command.Parameters.AddWithValue("@CPF", cliente.CPF ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Nome", cliente.Nome ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@RG", cliente.RG ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@DataExpedicaoRG", cliente.DataExpedicaoRG);
            command.Parameters.AddWithValue("@OrgaoExpedidor", cliente.OrgaoExpedidor ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@UF", cliente.UF ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
            command.Parameters.AddWithValue("@Sexo", cliente.Sexo ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@EstadoCivil", cliente.EstadoCivil ?? (object)DBNull.Value);
        }

        private static ClienteModel DadosClienteModel(SqlDataReader reader)
        {
            return new ClienteModel
            {
                IdCliente = reader["IdCliente"] != DBNull.Value ? (int)reader["IdCliente"] : 0,
                Nome = reader["Nome"] != DBNull.Value ? reader["Nome"].ToString() : string.Empty,
                CPF = reader["CPF"] != DBNull.Value ? reader["CPF"].ToString() : string.Empty,
                RG = reader["RG"] != DBNull.Value ? reader["RG"].ToString() : string.Empty,
                DataExpedicaoRG = (DateTime)(reader["DataExpedicaoRG"] != DBNull.Value ? Convert.ToDateTime(reader["DataExpedicaoRG"]).Date : (DateTime?)null),
                OrgaoExpedidor = reader["OrgaoExpedidor"] != DBNull.Value ? reader["OrgaoExpedidor"].ToString() : string.Empty,
                UF = reader["UF"] != DBNull.Value ? reader["UF"].ToString() : string.Empty,
                DataNascimento = (DateTime)(reader["DataNascimento"] != DBNull.Value ? Convert.ToDateTime(reader["DataNascimento"]).Date : (DateTime?)null),
                Sexo = reader["Sexo"] != DBNull.Value ? reader["Sexo"].ToString() : string.Empty,
                EstadoCivil = reader["EstadoCivil"] != DBNull.Value ? reader["EstadoCivil"].ToString() : string.Empty
            };
        }
    }
}
