using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.G04.Core.Entity.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Repository.Data.Configurations
{
    public class OrderItemConfigrations :IEntityTypeConfiguration<OrderItems>
    {
        public void Configure(EntityTypeBuilder<OrderItems> builder)
        {
            // Configure Product as an owned entity of OrderItems.
            // Product belongs to OrderItems and is not configured as an independent entity here.

            builder.OwnsOne(OI => OI.Product, P => P.WithOwner());

            // Configure Price as a decimal column with precision 18
            // and 2 decimal places in the database.

            builder.Property(P => P.Price).HasColumnType("decimal(18,2)");
        }
    }
}
