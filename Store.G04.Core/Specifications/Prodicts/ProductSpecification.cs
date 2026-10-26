using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Specifications.Prodicts
{
    public class ProductSpecification : BaseSpecification<Entity.Product, int>
    {
        public ProductSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude();

        }
        public ProductSpecification(ProductSpecParams specParams) : base(
            p =>
             (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search))
            &&
            (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId)
            && 
            (!specParams.TypeId.HasValue || p.TypeId == specParams.TypeId))  
        {


            if (!string.IsNullOrEmpty(specParams.sort))
            {
                switch(specParams.sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDESC(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }

            }
            else
            {
                AddOrderBy(p => p.Name);
            }


            AddInclude();

            ApplyPaging(specParams.PageSize* (specParams.PageIndex - 1), specParams.PageSize);


        }
        private void AddInclude()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Type);
        }
    } 
}
