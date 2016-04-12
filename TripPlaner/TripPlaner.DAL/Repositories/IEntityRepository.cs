using System.Linq;

namespace TripPlaner.DAL.Repositories
{
    public interface IEntityRepository<TEntity>
    {
        TEntity Insert(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(int id);

        TEntity Find(int id);

        IQueryable<TEntity> AsQueryable();
    }
}
