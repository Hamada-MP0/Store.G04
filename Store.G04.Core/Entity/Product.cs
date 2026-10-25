using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Entity
{
    public class Product : BaseEntity<int>
    {

      
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        
        public int? BrandId { get; set; }//FK // يمكن رقم أو null
        public ProductBrand Brand { get; set; }
        public int? TypeId { get; set; }//FK // يمكن رقم أو null
        public ProductType Type { get; set; }

    }
}
