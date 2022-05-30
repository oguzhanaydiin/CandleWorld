using CatalogService.Api.Core.Domain;
using CatalogService.Api.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Api.Infrastructure.EntiyConfiguration;

public class CatalogBrandEntityTypeConfiguration : IEntityTypeConfiguration<CatalogBrand>
{
    public void Configure(EntityTypeBuilder<CatalogBrand> builder)
    {
        //Schema.Table
        builder.ToTable("CatalogBrand", CatalogContext.DEFAULT_SCHEMA);

        //configure the primary key
        builder.HasKey(i => i.Id);

        //generate value for the id
        builder.Property(i => i.Id)
            .UseHiLo("catalog_brand_hilo")
            .IsRequired();

        builder.Property(i => i.Brand)
            .IsRequired()
            .HasMaxLength(100);
    }
}
