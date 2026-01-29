
using Microsoft.EntityFrameworkCore;
using NotificationService.Application.Abstractions.Persistence;
using NotificationService.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Persistence.Repositories
{
    internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity item)
        {
           
            await _context.Set<TEntity>().AddAsync(item);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entites)
        {
            
            await _context.Set<TEntity>().AddRangeAsync(entites);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
          return  await _context.Set<TEntity>().CountAsync(predicate);
        }

        public void Delete(TEntity item)
        {
           
           _context.Set<TEntity>().Remove(item);
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
           return await _context.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true)
        {
           
            return asNoTracking
                ? await _context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync()
                : await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }




        public async Task<TResult?> FirstOrDefaultAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, bool asNoTracking = true)
        {


            if (asNoTracking)
                return await _context.Set<TEntity>().AsNoTracking().Where(predicate).Select(selector).FirstOrDefaultAsync();



            return await _context.Set<TEntity>().Where(predicate).Select(selector).FirstOrDefaultAsync();

        }

        public IQueryable<TEntity> GetAllAsIQueryableAsync(bool AsNoTracking = true)
        {

            if (AsNoTracking)
                return _context.Set<TEntity>().AsNoTracking();


            return _context.Set<TEntity>();
        }



        public async Task<IEnumerable<TEntity>> GetAllAsync(bool AsNoTracking = true)
        {


            if (AsNoTracking)
                return await _context.Set<TEntity>().AsNoTracking().ToListAsync();


            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetById<TKey>(TKey id) => await _context.Set<TEntity>().FindAsync(id);


        public async Task<List<TResult>> ListAsync<TResult>(Expression<Func<TEntity, TResult>> selector, bool asNoTracking = true)
        {



            if (asNoTracking)
                return await _context.Set<TEntity>().AsNoTracking().Select(selector).ToListAsync();


            return await _context.Set<TEntity>().Select(selector).ToListAsync();
        }








        public async Task<List<TResult>> ListAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, bool asNoTracking = true)
        {



            if (asNoTracking)
                return await _context.Set<TEntity>().AsNoTracking().Where(predicate).Select(selector).ToListAsync();


            return await _context.Set<TEntity>().Where(predicate).Select(selector).ToListAsync();
        }



        public void Update(TEntity entity)
        {
            
            _context.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> items)
        {
            
            _context.Set<TEntity>().UpdateRange(items);
        }
    }
}
