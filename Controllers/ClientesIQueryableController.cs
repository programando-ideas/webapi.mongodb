using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using webapi.mongodb.Data;
using webapi.mongodb.Entities;

namespace webapi.mongodb.Controllers
{
    [Route("api/clientesiq")]
    [ApiController]
    public class ClientesIQueryableController : ControllerBase
    {
        private readonly ClientesDbQueryable _clienteDbQueryable;

        public ClientesIQueryableController(ClientesDbQueryable clienteDbQueryable)
        {
            _clienteDbQueryable = clienteDbQueryable;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _clienteDbQueryable.GetAsync());
        }

        [HttpGet("{id:length(24)}", Name = "GetClienteIQueryable")]
        public async Task<IActionResult> GetById(string id)
        {
            var cliente = await _clienteDbQueryable.GetByIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpGet()]
        [Route("getbydir/{calle}")]
        public async Task<IActionResult> GetClienteByDirAsync(string calle)
        {
            var cliente = await _clienteDbQueryable.GetClienteByDirAsync(calle);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            await _clienteDbQueryable.CreateAsync(cliente);

            return CreatedAtRoute("GetCliente", new
            {
                id = cliente.Id.ToString()
            }, cliente);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Cliente cli)
        {
            var cliente = _clienteDbQueryable.GetByIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            await _clienteDbQueryable.UpdateAsync(id, cli);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            var cliente = await _clienteDbQueryable.GetByIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            await _clienteDbQueryable.DeleteByIdAsync(cliente.Id);

            return NoContent();
        }
    }
}