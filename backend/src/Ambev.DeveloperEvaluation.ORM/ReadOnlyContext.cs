
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

        public bool CreateCollection<T>(string collectionName, IEnumerable<T> data)
        {
            try
            {
                _db.CreateCollection(collectionName);
                var collection = _db.GetCollection<T>(collectionName);
                collection.InsertMany(data);

                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public IMongoCollection<T> GetCollection<T>(string collectionName) => _db.GetCollection<T>(collectionName);

    }
}
