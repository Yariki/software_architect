using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.Persistence.Configurations;
public class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
{
    public void Configure(EntityTypeBuilder<Catalog> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(e => e.Image)
            .IsRequired(false);

        builder.Property(e => e.CatalogId)
            .IsRequired(false)
            .HasDefaultValue(null);

        builder.HasOne(e => e.Parent)
            .WithMany(e => e.Childrens)
            .HasForeignKey(e => e.CatalogId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
