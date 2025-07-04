using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.SaleNumber)
                  .IsRequired()
                  .HasMaxLength(50);

        builder.Property(s => s.Date)
                  .IsRequired();

        builder.Property(s => s.CustomerExternalId)
                  .IsRequired()
                  .HasMaxLength(50);

        builder.Property(s => s.BranchExternalId)
                  .IsRequired()
                  .HasMaxLength(50);

        builder.Property(s => s.IsCancelled)
                  .IsRequired();

        
        builder.Ignore(s => s.TotalAmount);

        builder.OwnsMany(s => s.Items, itemsNav =>
        {
            itemsNav.ToTable("SaleItems");
            itemsNav.WithOwner().HasForeignKey("SaleId");
            itemsNav.HasKey("Id");

            itemsNav.Property<Guid>("Id")
                    .HasColumnName("Id")
                    .ValueGeneratedNever();

            itemsNav.Property(i => i.ProductExternalId)
                    .IsRequired()
                    .HasMaxLength(50);

            itemsNav.Property(i => i.ProductDescription)
                    .IsRequired()
                    .HasMaxLength(200);

            itemsNav.Property(i => i.Quantity)
                    .IsRequired();

            itemsNav.Property(i => i.UnitPrice)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

            itemsNav.Property(i => i.Discount)
                    .IsRequired()
                    .HasColumnType("decimal(5,2)");

            itemsNav.Property(i => i.IsCancelled)
                    .IsRequired();

            itemsNav.Ignore(i => i.TotalAmount);
        });
    }
}