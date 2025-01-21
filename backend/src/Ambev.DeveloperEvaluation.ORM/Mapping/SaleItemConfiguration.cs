
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItemVO>
    {
        public void Configure(EntityTypeBuilder<SaleItemVO> builder)
        {
            builder.ToTable("SaleItems");
            builder.HasKey(si => new { si.SaleId, si.ProductId });
            builder.Property(si => si.SaleId).HasColumnType("uuid").ValueGeneratedNever();
            builder.Property(si => si.ProductId).HasColumnType("uuid").ValueGeneratedNever();

            builder.Property(si => si.ProductName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(si => si.ProductCategory)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(si => si.Total)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(si => si.Quantity)
                .HasColumnType("smallint")
                .IsRequired();


            builder.Property(si => si.ProductPrice)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();


            builder.Ignore(si => si.Total);
        }
    }
}
