using CadastroClienteFCC.Api.Model;
using CadastroClienteFCC.Api.Services.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CadastroClienteFCC.Api.Controllers
{
    [EnableCors("ApiCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<ClienteModel>>> Get()
        {
            List<ClienteModel> clientes = await  _clienteService.GetClienteAsync();
            if (clientes == null || !clientes.Any())
            {
                return NoContent();
            }
            return Ok(clientes);
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<ClienteModel>> GetId(int id)
        {
            ClienteModel cliente = await _clienteService.GetIdClienteAsync(id);
            if (cliente == null)
            {
                return NotFound(new { message = "Cliente não encontrado." });
            }
            return Ok(cliente);
        }

        [HttpPost("Post")]
        public async Task<IActionResult> Post([FromBody] ClienteModel cliente)
        {
            if (cliente == null)
            {
                return BadRequest(new { message = "Dados do cliente inválidos." });
            }

            var result = await _clienteService.PostClienteAsync(cliente);
            if (!result)
            {
                return StatusCode(500, new { message = "Erro ao inserir os dados do cliente." });
            }

            return CreatedAtAction(nameof(GetId), new { id = cliente.IdCliente }, new { message = "Cliente inserido com sucesso." });
        }

        [HttpPut("Put")]
        public async Task<IActionResult> Put([FromBody] ClienteModel cliente)
        {
            if (cliente == null)
            {
                return BadRequest(new { message = "Dados do cliente inválidos." });
            }

            var result = await _clienteService.PutClienteAsync(cliente);
            if (!result)
            {
                return NotFound(new { message = "Cliente não encontrado para atualização." });
            }

            return Ok(new { message = "Cliente atualizado com sucesso." });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _clienteService.DeleteClienteAsync(id);
            if (!result)
            {
                return NotFound(new { message = "Cliente não encontrado para exclusão." });
            }
            return Ok(new { message = "Cliente deletado com sucesso." });
        }
    }
}
