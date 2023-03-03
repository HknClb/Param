using Application.Abstractions.UnitOfWorks.Base;
using Persistence.Contexts;
using Persistence.UnitOfWorks.Base;

namespace Persistence.UnitOfWorks
{
    public class UnitOfWork : EfUnitOfWork, IUnitOfWork
    {
        public UnitOfWork(MovieDbContext movieDbContext) : base(movieDbContext)
        {
        }
    }
}