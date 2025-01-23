
using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.ORM.ReadOnlyRepositories
{
    public class SaleReadOnlyRepository : ReadOnlyRepository<SaleQuery>, ISaleReadOnlyRepository
    {
        public const string COLLECTION_NAME = "Sales";
        public SaleReadOnlyRepository(ReadOnlyContext ctx)
            : base(ctx, COLLECTION_NAME)
        {
        }

        public async Task<bool> CreateReadOnlyData(SaleQuery data)
        {
            await base._collection.InsertOneAsync(data);

            return true;
        }
        public async Task<bool> UpdateReadOnlyData(SaleQuery data)
        {
            var filter = Builders<SaleQuery>.Filter
                .Eq(sale => sale.id, data.id);

            
           var result = await base._collection.ReplaceOneAsync(filter, data);
            
            return result.ModifiedCount > 0;
        }
    }
}
