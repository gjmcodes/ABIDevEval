
using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.ORM.ReadOnlyRepositories
{
    public abstract class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : Query
    {
        protected readonly ReadOnlyContext _ctx;
        protected readonly IMongoCollection<T> _collection;
        protected ReadOnlyRepository(ReadOnlyContext ctx, string collectionName)
        {
            _ctx = ctx;
            _collection = _ctx.GetCollection<T>(collectionName);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var result = await _collection.Find(_ => true).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<T>> GetAllByIds(Guid[] ids)
        {
            var _ids = ids.Select(x => x.ToString());

            var result = await _collection.Find(x => _ids.Contains(x.id)).ToListAsync();

            return result;
        }

        public async Task<T> GetById(Guid id)
        {
            var query = await _collection.FindAsync(x => x.id == id.ToString());
            var result = await query.SingleOrDefaultAsync();

            return result;
        }
    }
}
