using CatalogService.Api.Core.Domain;
using CatalogService.Api.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Api.Infrastructure.EntiyConfiguration
{
    public class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("Catalog", CatalogContext.DEFAULT_SCHEMA);

            builder.Property(i => i.Id)
                .UseHiLo("catalog_hilo")
                .IsRequired();

            builder.Property(i => i.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(i => i.Price)
                .IsRequired(true);

            builder.Property(i => i.PictureFileName)
                .IsRequired(false);

            builder.Ignore(i => i.PictureUri);

            builder.HasOne(i => i.CatalogBrand)
                .WithMany()
                .HasForeignKey(i => i.CatalogBrandId);

            builder.HasOne(i => i.CatalogType)
                .WithMany()
                .HasForeignKey(i => i.CatalogTypeId);
        }
    }
}
