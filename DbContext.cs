using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using MongoHotchocolate.Graphql;

namespace MongoHotchocolate;

public class DbContext
{
    public IMongoCollection<ProductCategory> ProductCategories { get; set; }

    public DbContext()
    {
        var connectionString = "mongodb://localhost:27017";
        var databaseName = "default";

        var mongoConnectionUrl = new MongoUrl(connectionString);
        var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);
        mongoClientSettings.ClusterConfigurator = cb =>
        {
            cb.Subscribe<CommandStartedEvent>(e =>
            {
                Console.WriteLine("");
                Console.WriteLine($"Mongo Command : {e.CommandName}");
                Console.WriteLine(e.Command);
            });
        };
        var client = new MongoClient(mongoClientSettings);
        var database = client.GetDatabase(databaseName);

        ProductCategories = database.GetCollection<ProductCategory>(nameof(ProductCategory));
    }
}