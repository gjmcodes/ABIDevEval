
namespace Ambev.DeveloperEvaluation.Application.Sales.Shared.Results
{
    public struct SaleResult
    {
        public Guid Id { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SaleTotal { get;  set; }
        public bool Cancelled { get; set; }
        public string SaleNumber { get; set; }
        public decimal ListPrice { get; set; }
        public string[] SaleProducts { get; set; }
        public SaleCustomerResult SaleCustomer { get; set; }
        public SaleBranchResult SaleBranch { get; set; }
        public ICollection<SaleItemResult> Items { get; set; }
    }

    public struct SaleCustomerResult
    {
        public Guid SaleId { get; private set; }
        public Guid CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public string CustomerEmail { get; private set; }
    }
    public struct SaleBranchResult
    {
        public Guid SaleId { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
    }
    public struct SaleItemResult
    {
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get;  set; }
        public ushort DiscountPercentage { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Cancelled { get; set; }
        public decimal ListPrice { get; set; }
    }
}
