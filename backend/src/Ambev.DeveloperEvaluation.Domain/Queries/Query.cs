
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ambev.DeveloperEvaluation.Domain.Queries
{
    public abstract class Query
    {
        [BsonId]
        public string id;

        public Guid GuidId => Guid.Parse(id);
    }
}
