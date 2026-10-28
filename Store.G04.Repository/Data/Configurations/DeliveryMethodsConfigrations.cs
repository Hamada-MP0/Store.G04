using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.G04.Core.Entity.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Repository.Data.Configurations
{
    public class DeliveryMethodsConfigrations : IEntityTypeConfiguration <DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            // Configure the Cost property as a decimal column
            // with 18 total digits and 2 digits after the decimal point.
            builder.Property(DM => DM.Cost)
                .HasColumnType("decimal(18,2)");
        }
    }
}
