﻿
using Ambev.DeveloperEvaluation.Domain.Queries;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IReadOnlyBranchRepository : IReadOnlyRepository<BranchExternalQuery>
    {
    }
}
