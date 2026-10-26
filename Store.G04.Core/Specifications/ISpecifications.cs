using Store.G04.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

    namespace Store.G04.Core.Specifications
    {
        public interface ISpecifications<TEntity , Tkey> where TEntity : BaseEntity<Tkey>   
        {
            public Expression<Func<TEntity, bool>> Criteria { get; set; }
            public List<Expression<Func<TEntity, object>>> Includes { get; set; }
        public Expression<Func<TEntity, object>> OrderByDESC { get; set; }
       
        public Expression<Func<TEntity, object>> OrderBy { get; set; }

        public int skip { get; set; }
        public int take { get; set; }
        public bool IsPagingEnabled { get; set; }

    }
    }
