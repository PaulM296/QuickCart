﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickCart.Domain.Entities.OrderAggregates;

namespace QuickCart.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(x => x.ItemOrdered, o => o.WithOwner());
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
        }
    }
}
