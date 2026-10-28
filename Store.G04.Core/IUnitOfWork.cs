using Store.G04.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Text;

namespace Store.G04.Core
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        Task<int> CompletelyAsync();

        // Create a generic repository for the specified entity type and key type
        IGenericRepository<TEntity,Tkey>Repository<TEntity, Tkey>() where TEntity : Entity.BaseEntity<Tkey>;


    }
}
