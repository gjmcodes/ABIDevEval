
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Queries;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.ORM
{
    public class ReadOnlyContext
    {
        private readonly IMongoDatabase _db;
        private readonly MongoClient _client;
        public ReadOnlyContext(string server, string database)
        {
            _client = new MongoClient(server);
            _db = _client.GetDatabase(database);
        }
        public IMongoCollection<T> GetCollection<T>(string collectionName) => _db.GetCollection<T>(collectionName);

    }
}
