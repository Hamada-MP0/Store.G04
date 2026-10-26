using Store.G04.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Specifications.Prodicts
{
    public class ProductWithCountSpecification : BaseSpecification<Product,int>
    {
        public ProductWithCountSpecification(ProductSpecParams specParams) : base(
          p =>
          (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search))
          &&
          (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId)
          &&
          (!specParams.TypeId.HasValue || p.TypeId == specParams.TypeId))
        {

        }
    }
}
