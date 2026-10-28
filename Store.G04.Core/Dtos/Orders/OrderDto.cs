using Store.G04.Core.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Dtos.Orders
{
    public class OrderDto
    {
        public string BasketId { get; set; }

        public int DeliveryMethodId { get; set; }

        public AddressDto ShipToAddress { get; set; }

    }
}
