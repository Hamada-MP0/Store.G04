using Store.G04.Core.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Dtos.Orders
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

        public string Status { get; set; }

        public AddressDto ShippingAddress { get; set; }


        public string DeliveryMethod { get; set; }

        public string DeliveryMethodCost { get; set; }


        public ICollection<OrderItemDto> Items { get; set; }


        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }

        public string? PaymentIntentId { get; set; } = string.Empty;

    }
}
