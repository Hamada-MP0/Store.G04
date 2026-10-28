using Store.G04.Core.Entity;
using Store.G04.Core.Entity.Order;
using Store.G04.Core.Specifications;
using Store.G04.Core.Specifications.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Repositories.Contract
{
    public interface IGenericRepository<TEntity , Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Tkey id);
        Task<IEnumerable<TEntity>> GetAllًWithSpecAsync(ISpecifications<TEntity, Tkey> specification);
        Task<TEntity> GetByIdWithSpecAsync(ISpecifications<TEntity, Tkey> specification);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);

        void Delete(TEntity entity);
        Task GetAllًWithSpecAsync();

        Task<int> CountAsync(ISpecifications<TEntity, Tkey> specification);
        Task<Order?> GetWithSpecAsync(OrderSpecification spec);
    }
}
