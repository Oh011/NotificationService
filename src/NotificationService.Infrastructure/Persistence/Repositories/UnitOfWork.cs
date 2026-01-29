
using NotificationService.Application.Abstractions.Persistence;
using NotificationService.Infrastructure.Persistence.Context;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Persistence.Repositories
{
    internal class UnitOfWork : IUnitOfWork 
    {


        private readonly ConcurrentDictionary<string, object> _repositories;
        private readonly ApplicationDbContext _context;



        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _repositories = new ConcurrentDictionary<string, object>();

        }

        public async Task<int> Commit(CancellationToken cancellationToken = default)
        {
           

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
           
             _context.Dispose();
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
           
            var typeName = typeof(TEntity).Name;
           
          return(IGenericRepository<TEntity>)  _repositories.GetOrAdd(typeName,(string ket)=> new GenericRepository<TEntity>(_context));
        }
    }
}
