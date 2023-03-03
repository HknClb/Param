using Application.Abstractions.Repositories;
using Domain.Entities.Common;

namespace Application.Abstractions.UnitOfWorks.Base
{
    public interface IUnitOfWorkBase : IDisposable
    {
        IGenericReadRepository<TEntity> ReadRepository<TEntity>() where TEntity : Entity;
        IGenericWriteRepository<TEntity> WriteRepository<TEntity>() where TEntity : Entity;
        public void Complete();
        public Task CompleteAsync(CancellationToken cancellationToken = default);
    }
}
