using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.mongodb.Data.Configuration;
using webapi.mongodb.Entities;

namespace webapi.mongodb.Data
{
    public class ClientesDbAsync
    {
        private readonly IMongoCollection<Cliente> _clientesCollection;

        public ClientesDbAsync(IClientesStoreDatabaseSettings settings,
                               IClientSettingsService clientSettings)
        {
            var mdbClient = clientSettings.Client;

            var database = mdbClient.GetDatabase(settings.DatabaseName);

            _clientesCollection = database.GetCollection<Cliente>(settings.ClientesCollectionName);
        }

        public async Task<List<Cliente>> GetAsync()
        {
            return await _clientesCollection.Find(cli => true).ToListAsync();
        }

        public async Task<Cliente> GetByIdAsync(string id)
        {
            var cli = await _clientesCollection.FindAsync(cliente => cliente.Id == id);

            return await cli.FirstOrDefaultAsync();
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
