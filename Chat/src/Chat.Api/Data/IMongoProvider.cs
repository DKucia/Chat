using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Chat.Api.Data
{
    public interface IMongoProvider<T>
    {
        IMongoCollection<T> Collection{get;}
        IMongoDatabase Database { get; }
        IMongoQueryable<T> Querable { get; }
    }
    
}