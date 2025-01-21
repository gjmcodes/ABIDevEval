
namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class SaleBranchVO
    {
        public SaleBranchVO(Guid saleId, Guid branchId, string branchName)
        {
            SaleId = saleId;
            BranchId = branchId;
            BranchName = branchName;
        }
        public Guid SaleId { get; }
        public Guid BranchId { get;}
        public string BranchName { get;}

    }
}
