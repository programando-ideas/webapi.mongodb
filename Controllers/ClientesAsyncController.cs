using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using webapi.mongodb.Data;
using webapi.mongodb.Entities;

namespace webapi.mongodb.Controllers
{
    [Route("api/clientesasync")]
    [ApiController]
    public class ClientesAsyncController : ControllerBase
    {
        private readonly ClientesDbAsync _clienteDbAsync;

        public ClientesAsyncController(ClientesDbAsync clienteDbAsync)
        {
            _clienteDbAsync = clienteDbAsync;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _clienteDbAsync.GetAsync());
        }

        [HttpGet("{id:length(24)}", Name = "GetClienteAsync")]
        public async Task<IActionResult> GetById(string id)
        {
            var cliente = await _clienteDbAsync.GetByIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            await _clienteDbAsync.CreateAsync(cliente);

            return CreatedAtRoute("GetCliente", new
            {
                id = cliente.Id.ToString()
            }, cliente);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Cliente cli)
        {
            var cliente = _clienteDbAsync.GetByIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            await _clienteDbAsync.UpdateAsync(id, cli);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            var cliente = await _clienteDbAsync.GetByIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            await _clienteDbAsync.DeleteByIdAsync(cliente.Id);

            return NoContent();
        }
    }
}