using Application.Abstractions.Repositories.Base.Reads;
using Domain.Entities.Common;

namespace Application.Abstractions.Repositories
{
    public interface IGenericReadRepository<TEntity> : IReadRepository<TEntity>, IAsyncReadRepository<TEntity> where TEntity : Entity
    {
    }
}
