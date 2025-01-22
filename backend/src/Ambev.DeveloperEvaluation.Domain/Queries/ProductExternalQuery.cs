using MongoDB.Bson.Serialization.Attributes;

namespace Ambev.DeveloperEvaluation.Domain.Queries
{
    public class ProductExternalQuery : Query
    {
        [BsonElement("name")]
        public string name;

        [BsonElement("category")]
        public string category;

        [BsonElement("price")]
        public decimal price;
    }
}
