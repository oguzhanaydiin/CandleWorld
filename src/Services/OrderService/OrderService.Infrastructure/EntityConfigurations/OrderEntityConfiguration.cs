using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregateModels.OrderAggregate;
using OrderService.Infrastructure.Context;

namespace OrderService.Infrastructure.EntityConfigurations;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders", OrderDbContext.DEFAULT_SCHEMA);

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).ValueGeneratedOnAdd();

        builder.Ignore(i => i.DomainEvents);

        //address id si olmayan value object oldugu icin ownsone kullandik
        builder.OwnsOne(i => i.Address, x =>
                {
                    x.WithOwner();
                });

        builder.Property<int>("orderStatusId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("OrderStatusId")
            .IsRequired();

        var navigation = builder.Metadata.FindNavigation(nameof(Order.OrderItems));

        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasOne(i => i.Buyer)
            .WithMany()
            .HasForeignKey(i => i.BuyerId);

        builder.HasOne(i => i.OrderStatus)
            .WithMany()
            .HasForeignKey("orderStatusId");
    }
}
