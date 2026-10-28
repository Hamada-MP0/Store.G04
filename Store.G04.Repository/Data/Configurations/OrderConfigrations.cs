using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.G04.Core.Entity.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Repository.Data.Configurations
{
    public class OrderConfigrations: IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {   // Configure the SubTotal property as a decimal column
            // with a precision of 18 digits and 2 decimal places.

            builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");

            // Convert the OrderStatus enum to a string when saving to the database,
            // and convert the string back to OrderStatus when retrieving the data.
            builder.Property(O => O.Status)
                .HasConversion(OStatus => OStatus.ToString(), OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus));
            // Configure ShippingAddress as an owned entity of Order.
            // The shipping address belongs to the Order and is not an independent entity here.
            builder.OwnsOne(O => O.ShippingAddress, SA => SA.WithOwner());
            // Configure a relationship where each Order has one DeliveryMethod,
            // while one DeliveryMethod can be associated with multiple Orders.
            // DeliveryMethodId is the foreign key used for this relationship.
            builder.HasOne(O => O.DeliveryMethod).WithMany().HasForeignKey(O => O.DeliveryMethodId);

            // Configure a one-to-many relationship between Order and its Items.
            // Each Order can contain multiple Items, and each Item belongs to one Order.
            // Cascade Delete automatically deletes the related Items when the Order is deleted.

            builder.HasMany(O => O.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
