using QuickCart.Domain.Entities;
using QuickCart.Domain.Interfaces;
using System.Collections.Concurrent;

namespace QuickCart.Infrastructure.Data
{
    public class UnitOfWork(QuickCartDbContext context) : IUnitOfWork
    {
        private readonly ConcurrentDictionary<string, object> _repositories = new();
        public async Task<bool> Complete()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IBaseRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;

            return (IBaseRepository<TEntity>)_repositories.GetOrAdd(type, t =>
            {
                var repositoryType = typeof(BaseRepository<>).MakeGenericType(typeof(TEntity));
                return Activator.CreateInstance(repositoryType, context)
                    ?? throw new InvalidOperationException($"Could not create repository instance for {t}.");
            });
        }
    }
}
