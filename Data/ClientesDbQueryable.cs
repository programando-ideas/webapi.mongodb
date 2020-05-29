using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using webapi.mongodb.Data.Configuration;
using webapi.mongodb.Entities;

namespace webapi.mongodb.Data
{
    public class ClientesDbQueryable
    {
        private readonly IMongoQueryable<Cliente> _clientesQueryable;
        private readonly IMongoCollection<Cliente> _clientesCollection;

        public ClientesDbQueryable(IClientesStoreDatabaseSettings settings,
                                   IClientSettingsService clientSettings)
        {
            var mdbClient = clientSettings.Client;

            var database = mdbClient.GetDatabase(settings.DatabaseName);

            _clientesCollection = database.GetCollection<Cliente>(settings.ClientesCollectionName);
            _clientesQueryable = _clientesCollection.AsQueryable();
        }

        public async Task<List<Cliente>> GetAsync()
        {
            return await _clientesQueryable.ToListAsync();
        }

        public async Task<Cliente> GetByIdAsync(string id)
        {
            var cli = _clientesQueryable.Where(cli => cli.Id == id);

            if (!cli.Any())
                return null;

            Cliente cliRet = await cli.FirstOrDefaultAsync();

            return cliRet;
        }

        public async Task<Cliente> GetClienteByDirAsync(string calle)
        {
            var cli = _clientesQueryable.Where(cli =>
                                    cli.DireccionCliente.Calle.Contains(calle));

            //if (!cli.Any())
            //    return null;

            Cliente cliRet = await cli.FirstOrDefaultAsync();

            return cliRet;
        }

        public async Task<Cliente> CreateAsync(Cliente cli)
        {
            await _clientesCollection.InsertOneAsync(cli);
            return cli;
        }

        public async Task UpdateAsync(string id, Cliente cli)
        {
            await _clientesCollection.ReplaceOneAsync(cliente => cliente.Id == id, cli);
        }

        public async Task DeleteAsync(Cliente cli)
        {
            await _clientesCollection.DeleteOneAsync(cliente => cliente.Id == cli.Id);
        }

        public async Task DeleteByIdAsync(string id)
        {
            await _clientesCollection.DeleteOneAsync(cliente => cliente.Id == id);
        }
    }
}
