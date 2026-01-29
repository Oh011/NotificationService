using System.Linq.Expressions;

namespace NotificationService.Application.Abstractions.Persistence
{
    public interface IGenericRepository<T> 
    where T : class
    {


        //Write 

        Task AddAsync(T item);


        Task AddRangeAsync(IEnumerable<T> entites);
        void Update(T entity);

        void UpdateRange(IEnumerable<T> items);

        void Delete(T item);


        //----------------------------------------------

        //Read

        Task<T?> GetById<TKey>(TKey id);

        //Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, bool asNoTracking = false);

        //Task<TResult?> FirstOrDefaultAsync<TResult>(
        //    ISpecification<T> specification,
        //    Expression<Func<T, TResult>> selector,
        //    bool asNoTracking = true);

        Task<TResult?> FirstOrDefaultAsync<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            bool asNoTracking = true);

        Task<IEnumerable<T>> FindAsync(
            Expression<Func<T, bool>> predicate,
            bool asNoTracking = true);

        Task<IEnumerable<T>> GetAllAsync(bool asNoTracking = true);

        IQueryable<T> GetAllAsIQueryableAsync(bool asNoTracking = true);

        //Task<IEnumerable<T>> GetAllWithSpecifications(
        //    ISpecification<T> specification,
        //    bool asNoTracking = true);

        //Task<List<TResult>> ListAsync<TResult>(
        //    ISpecification<T> specifications,
        //    Expression<Func<T, TResult>> selector,
        //    bool asNoTracking = true);

        Task<List<TResult>> ListAsync<TResult>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> selector,
            bool asNoTracking = true);

        Task<List<TResult>> ListAsync<TResult>(
            Expression<Func<T, TResult>> selector,
            bool asNoTracking = true);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);


    }


}
