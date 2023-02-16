using Application.DynamicQuery;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Abstractions.Repositories
{
    public interface IAsyncReadRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>,
            IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = true, CancellationToken cancellationToken = default);

        Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = true, CancellationToken cancellationToken = default);

        Task<IQueryable<TEntity>> GetListAsQueryableAsync(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = true, CancellationToken cancellationToken = default);

        Task<IList<TEntity>> GetListByDynamicAsync(Dynamic dynamic, Func<IQueryable<TEntity>,
            IIncludableQueryable<TEntity, object>>? include = null,
            bool enableTracking = true, CancellationToken cancellationToken = default);
    }
}