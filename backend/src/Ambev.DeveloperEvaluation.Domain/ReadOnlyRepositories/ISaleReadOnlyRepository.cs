
using Ambev.DeveloperEvaluation.Domain.Queries;

namespace Ambev.DeveloperEvaluation.Domain.ReadOnlyRepositories
{
    public interface ISaleReadOnlyRepository : IReadOnlyRepository<SaleQuery>
    {
        Task<bool> CreateReadOnlyData(SaleQuery data);
        Task<bool> UpdateReadOnlyData(SaleQuery data);
    }
}
