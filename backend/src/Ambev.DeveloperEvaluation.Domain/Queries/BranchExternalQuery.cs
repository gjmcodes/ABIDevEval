using MongoDB.Bson.Serialization.Attributes;

namespace Ambev.DeveloperEvaluation.Domain.Queries
{
    public class BranchExternalQuery : Query
    {
        [BsonElement("name")]
        public string Name { get; set; }
    }
}
