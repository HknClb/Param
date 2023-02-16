using Domain.Entities.Common;

namespace Application.Abstractions.Repositories
{
    public interface IWriteRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        TEntity Add(TEntity entity);
        void AddRange(IList<TEntity> entities);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);
        void DeleteRange(IList<TEntity> entities);
        TEntity? DeleteById(string id);
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
    }
}