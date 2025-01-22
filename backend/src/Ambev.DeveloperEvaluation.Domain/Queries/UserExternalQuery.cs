using MongoDB.Bson.Serialization.Attributes;

namespace Ambev.DeveloperEvaluation.Domain.Queries
{
    public class UserExternalQuery : Query
    {
        [BsonElement("name")]
        public string name;
        [BsonElement("email")]
        public string email;
    }
}
