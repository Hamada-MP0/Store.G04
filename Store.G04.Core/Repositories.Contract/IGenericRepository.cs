using Store.G04.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Repositories.Contract
{
    public interface IGenericRepository<TEntity , Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Tkey id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);

        void Delete(TEntity entity);

    }
}
