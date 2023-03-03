using Application.Abstractions.Repositories.Base.Writes;
using Domain.Entities.Common;

namespace Application.Abstractions.Repositories
{
    public interface IGenericWriteRepository<TEntity> : IWriteRepository<TEntity>, IAsyncWriteRepository<TEntity> where TEntity : Entity
    {
    }
}
