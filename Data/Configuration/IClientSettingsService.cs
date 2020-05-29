using MongoDB.Driver;

namespace webapi.mongodb.Data.Configuration
{
    public interface IClientSettingsService
    {
        MongoClient Client { get; }
    }
}
