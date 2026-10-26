    using Microsoft.EntityFrameworkCore;
    using Store.G04.Core.Specifications;
    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace Store.G04.Repository
    {
        public class SpecificationEvaluator<TEntity, Tkey> where TEntity : Store.G04.Core.Entity.BaseEntity<Tkey>
        {
            public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery , ISpecifications<TEntity, Tkey> specification)
            {
                var query = inputQuery;

          
                    if (specification.Criteria is not null)
                    {
                        query = query.Where(specification.Criteria);
                    }

                    if(specification.OrderBy is not null)
            {
                query = query.OrderBy(specification.OrderBy);
            }

            if(specification.OrderByDESC is not null)
            {
                query = query.OrderByDescending(specification.OrderByDESC);
            }
  
            if(specification.IsPagingEnabled)
            {
                query = query.Skip(specification.skip).Take(specification.take);
            }
 
            query = specification.Includes.Aggregate(query, (current, includeEX) => current.Include(includeEX));
            


                return query;   

            }
        }
    }
