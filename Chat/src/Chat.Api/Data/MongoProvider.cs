using Chat.Api.Data;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Chat.Api.Data
{
    public class MongoProvider<T> : IMongoProvider<T>
    {
        protected readonly IMongoCollection<T> _collection;
        protected readonly IMongoDatabase _database;
        public IMongoCollection<T> Collection => _collection;
        public IMongoDatabase Database => _database;

        public IMongoQueryable<T> Querable => _collection.AsQueryable();

        public MongoProvider()
        {
            string connectionString = "mongodb://localhost:27017/chat";
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(new MongoUrl(connectionString).DatabaseName);
            _collection = _database.GetCollection<T>(typeof(T).Name);
        }
    }

}