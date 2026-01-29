namespace NotificationService.Application.Abstractions.Persistence
{
    public interface IUnitOfWork : IDisposable
    {


        public Task<int> Commit(CancellationToken cancellationToken = default);




        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

    }
}
