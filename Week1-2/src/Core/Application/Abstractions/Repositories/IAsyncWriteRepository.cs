using Domain.Entities.Common;

namespace Application.Abstractions.Repositories
{
    public interface IAsyncWriteRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task AddRangeAsync(IList<TEntity> entities);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(IList<TEntity> entities);
        Task<TEntity?> DeleteByIdAsync(string id);

        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}