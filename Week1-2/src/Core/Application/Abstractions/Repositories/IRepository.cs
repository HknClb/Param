using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        DbSet<TEntity> Table { get; }
    }
}
