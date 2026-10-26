using Store.G04.Core.Repositories.Contract;
using Store.G04.Repository.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Store.G04.Core.Specifications;

namespace Store.G04.Repository.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : Core.Entity.BaseEntity<Tkey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if(typeof(TEntity) == typeof(Core.Entity.Product))
            {
                return (IEnumerable<TEntity>)await _context.Products.OrderBy(static p => p.Name).Include(p => p.Brand)
                                      .Include(p => p.Type)
                                      .ToListAsync();
            }
            return await _context.Set<TEntity>().ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync(Tkey id)
        {
            if (typeof(TEntity) == typeof(Core.Entity.Product))
            {
                // return  await _context.Products.Include(p => p.Brand).Include(p => p.Type).FirstOrDefaultAsync(p => p.Id == id as int? ) as TEntity;
                return await _context.Products.Where(p => p.Id == id as int?).Include(p => p.Brand).Include(p => p.Type).FirstOrDefaultAsync() as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(id);

        }
        public async Task AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
        public void Delete(TEntity entity)
        {
           _context.Set<TEntity>().Remove(entity);
        }



        private IQueryable<TEntity> Apply(ISpecifications<TEntity,Tkey> Spec)
        {
            return SpecificationEvaluator<TEntity, Tkey>.GetQuery(_context.Set<TEntity>(), Spec);
        }
        public async Task<IEnumerable<TEntity>> GetAllًWithSpecAsync(ISpecifications<TEntity, Tkey> specification)
        {
            return await Apply(specification).ToListAsync();
        }

        public async Task<TEntity> GetByIdWithSpecAsync(ISpecifications<TEntity, Tkey> specification)
        {
            return await Apply(specification).FirstOrDefaultAsync();

        }

        //public Task GetAllًWithSpecAsync()
        //{
        //    throw new NotImplementedException();


        //}

        public async Task<int> CountAsync(ISpecifications<TEntity, Tkey> specification)
        {
           return await Apply(specification).CountAsync();
        }
    }
}
