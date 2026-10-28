using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Store.G04.Core.Entity.Order
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Payment Received")]
        PaymentReceived,
        [EnumMember(Value = "Payment Failed")]
        PaymentFailed,

    }
}
