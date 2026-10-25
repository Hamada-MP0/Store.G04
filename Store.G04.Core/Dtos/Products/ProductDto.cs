using Store.G04.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Dtos.Products
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int? BrandId { get; set; }//FK // يمكن رقم أو null
        public string BrandName { get; set; }
        public int? TypeId { get; set; }//FK // يمكن رقم أو null
        public string TypeName { get; set; }

    }
}
