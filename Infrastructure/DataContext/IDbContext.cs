using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataContext
{
    public interface IDbContext : IDisposable
    {
        DbSet<TEntity> SetEntity<TEntity>() where TEntity : class;
        Task<int> CommitChangeAsync();
    }
}
