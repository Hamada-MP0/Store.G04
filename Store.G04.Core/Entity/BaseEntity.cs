using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Entity
{
    public class BaseEntity<Tkey>
    {
        public Tkey Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow  ;
    }
}
