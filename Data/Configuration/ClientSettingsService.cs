using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using System;

namespace webapi.mongodb.Data.Configuration
{
    public class ClientSettingsServiceMongoDB : IClientSettingsService
    {
        public MongoClient Client { get; }
        public ClientSettingsServiceMongoDB(IClientesStoreDatabaseSettings settings) 
        {
            Client = GetClient(settings);
        }

        private MongoClient GetClient(IClientesStoreDatabaseSettings settings)
        {
            // Extrae el host y puerto del connectionstring
            var uriMongoDB = new Uri(settings.ConnectionString);

            string host = uriMongoDB.Host;
            int port = uriMongoDB.Port;
            string[] userInfo = uriMongoDB.UserInfo.Split(':');
            string user = userInfo[0];
            string pwd = userInfo[1];

            // Crea las credenciales para conectarse a MongoDB
            var credentials = MongoCredential.CreateCredential(settings.DatabaseName, user, pwd);

            // Instancia el cliente de MongoDB
            var mdbClient = new MongoClient(new MongoClientSettings()
            {
                Server = new MongoServerAddress(host, port),
                Credential = credentials,
                ClusterConfigurator = cb =>
                {
                    cb.Subscribe<CommandStartedEvent>(e =>
                    {
                        ConsoleColor c = Console.BackgroundColor;
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.Write("MongoDb:");
                        Console.BackgroundColor = c;
                        Console.WriteLine($" ManagedThreadId: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                        Console.WriteLine($" CommandName: {e.CommandName}");
                        Console.WriteLine($"\t Command: {e.Command.ToJson()}");
                    });
                }
            });

            return mdbClient;
        }
    }
}
