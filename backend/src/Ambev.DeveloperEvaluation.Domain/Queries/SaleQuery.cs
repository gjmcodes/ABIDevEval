
using MongoDB.Bson.Serialization.Attributes;

namespace Ambev.DeveloperEvaluation.Domain.Queries
{

    public class SaleQuery : Query
    {
        [BsonElement("SaleDate")]
        public DateTime SaleDate { get; set; }
        [BsonElement("SaleTotal")]
        public decimal SaleTotal { get; set; }
        [BsonElement("Cancelled")]
        public bool Cancelled { get; set; }
        [BsonElement("SaleNumber")]
        public string SaleNumber { get; set; }
        [BsonElement("ListPrice")]
        public decimal ListPrice { get; set; }
        [BsonElement("SaleProducts")]
        public string[] SaleProducts { get; set; }
        [BsonElement("SaleCustomer")]
        public SaleCustomerQuery SaleCustomer { get; set; }
        [BsonElement("SaleBranch")]
        public SaleBranchQuery SaleBranch { get; set; }
        [BsonElement("Items")]
        public ICollection<SaleItemQuery> Items { get; set; }
    }

    public class SaleCustomerQuery
    {
        [BsonElement("SaleId")]
        public string SaleId { get; private set; }
        [BsonElement("CustomerId")]
        public string CustomerId { get; private set; }
        [BsonElement("CustomerName")]
        public string CustomerName { get; private set; }
        [BsonElement("CustomerEmail")]
        public string CustomerEmail { get; private set; }
    }
    public class SaleBranchQuery
    {
        [BsonElement("SaleId")]
        public string SaleId { get; set; }
        [BsonElement("BranchId")]
        public string BranchId { get; set; }
        [BsonElement("BranchName")]
        public string BranchName { get; set; }
    }
    public struct SaleItemQuery
    {
        [BsonElement("SaleId")]
        public string SaleId { get; set; }
        [BsonElement("ProductId")]
        public string ProductId { get; set; }
        [BsonElement("ProductName")]
        public string ProductName { get; set; }
        [BsonElement("ProductCategory")]
        public string ProductCategory { get; set; }
        [BsonElement("ProductPrice")]
        public decimal ProductPrice { get; set; }
        [BsonElement("Quantity")]
        public int Quantity { get; set; }
        [BsonElement("DiscountPercentage")]
        public ushort DiscountPercentage { get; set; }
        [BsonElement("TotalPrice")]
        public decimal TotalPrice { get; set; }
        [BsonElement("Cancelled")]
        public bool Cancelled { get; set; }
        [BsonElement("ListPrice")]
        public decimal ListPrice { get; set; }
    }
}
