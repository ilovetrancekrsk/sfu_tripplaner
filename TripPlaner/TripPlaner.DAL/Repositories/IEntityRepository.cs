using System.Linq;
using System.Threading.Tasks;

namespace TripPlaner.DAL.Repositories
{
    public interface IEntityRepository<TEntity>
    {
        TEntity Insert(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(int id);

        void Delete(TEntity entity);

        TEntity Find(int id);

        Task<TEntity> FindAsync(int id); 
        
        IQueryable<TEntity> AsQueryable();
    }
}
