using Store.G04.Core;
using Store.G04.Core.Entity;
using Store.G04.Core.Repositories.Contract;
using Store.G04.Repository.Data.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Repository
{
    public class UnitOfWork :  IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private Hashtable _repositoreys;

        public UnitOfWork(StoreDbContext context) {
            _context = context;
            _repositoreys = new Hashtable();

        }
        public Task<int> CommitAsync() => _context.SaveChangesAsync();

        public Task<int> CompletelyAsync()
        {
            throw new NotImplementedException();
        }

        public IGenericRepository<TEntity, Tkey> Repository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
          var type = typeof(TEntity).Name;
            if(!_repositoreys.ContainsKey(type))
            {
                
                var repository = new Repositories.GenericRepository<TEntity, Tkey>(_context);
                _repositoreys.Add(type, repository);
            }

            return (IGenericRepository<TEntity, Tkey>)_repositoreys[type];

        }
    }
}
