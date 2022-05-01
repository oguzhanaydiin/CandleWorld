using CatalogService.Api.Core.Domain;
using CatalogService.Api.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Api.Infrastructure.EntiyConfiguration
{
    public class CatalogBrandEntityTypeConfiguration : IEntityTypeConfiguration<CatalogBrand>
    {
        public void Configure(EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.ToTable("CatalogBrand", CatalogContext.DEFAULT_SCHEMA);

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .UseHiLo("catalog_brand_hilo")
                .IsRequired();

            builder.Property(b => b.Brand)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
