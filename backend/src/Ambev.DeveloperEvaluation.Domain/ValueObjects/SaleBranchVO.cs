
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class SaleBranchVO
    {
        private SaleBranchVO(){}

        public SaleBranchVO(Sale sale, Guid branchId, string branchName)
        {
            SaleId = sale.Id;
            BranchId = branchId;
            BranchName = branchName;
        }
        public Guid SaleId { get; private set; }
        public Guid BranchId { get; private set; }
        public string BranchName { get; private set; }

    }
}
