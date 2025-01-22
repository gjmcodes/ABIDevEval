using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.SaleNumber).IsRequired().HasMaxLength(36);
            builder.Property(s => s.ListPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(s => s.Cancelled)
            .IsRequired();

            builder.OwnsOne(s => s.SaleCustomer, customer =>
            {
                customer.ToTable("SalesCustomers");
                customer.HasKey(c => new { c.SaleId, c.CustomerId });
                customer.Property(c => c.SaleId).HasColumnType("uuid").ValueGeneratedNever();
                customer.Property(c => c.CustomerId).HasColumnType("uuid").ValueGeneratedNever();

                customer.Property(c => c.CustomerName).IsRequired().HasMaxLength(300);
                customer.Property(c => c.CustomerEmail).IsRequired().HasMaxLength(254);
            });



            builder.OwnsOne(s => s.Branch, branch =>
            {
                branch.ToTable("SalesBranches");
                branch.HasKey(b => new { b.SaleId, b.BranchId });
                branch.Property(b => b.SaleId).HasColumnType("uuid").ValueGeneratedNever();
                branch.Property(b => b.BranchId).HasColumnType("uuid").ValueGeneratedNever();

                branch.Property(b => b.BranchName)
                .IsRequired()
                .HasMaxLength(300);
            });

       

            builder.HasMany(s => s.Items)
                .WithOne(i => i.Sale)
                .HasForeignKey("SaleId")
                .OnDelete(DeleteBehavior.Cascade);


            builder.Ignore(s => s.SaleNumber);
            builder.Ignore(s => s.ListPrice);
            builder.Ignore(s => s.SaleProducts);
        }
    }
}
