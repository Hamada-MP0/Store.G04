using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Store.G04.Core.Specifications
{
    public class BaseSpecification<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : Entity.BaseEntity<Tkey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get ; set ; }= null;
        public List<Expression<Func<TEntity, object>>> Includes { get ; set ; } =new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>> OrderByDESC { get ; set; } = null;

        public Expression<Func<TEntity, object>> OrderBy { get; set ; } = null;
        public int skip { get; set ; }
        public int take { get ; set ; }
        public bool IsPagingEnabled { get ; set ; }

        public BaseSpecification(Expression<Func<TEntity, bool>> expression)
        {
            Criteria = expression;
          
        }

        public BaseSpecification()
        {
        }
        public void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        public void AddOrderByDESC(Expression<Func<TEntity, object>> orderByDescExpression)
        {
            OrderByDESC = orderByDescExpression;
        }

        public void ApplyPaging(int skip, int take)
        {
            IsPagingEnabled = true;

            this.skip = skip;
            this.take = take;
        }

    }
}
