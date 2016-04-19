using System.Linq;
using System.Threading.Tasks;

namespace TripPlaner.Services.Services
{
    public interface IEntityService<TEntity>
    {
        TEntity Insert(TEntity entity);
        TEntity InsertAndSave(TEntity entity);
        Task<TEntity> InsertAndSaveAsync(TEntity entity);

        TEntity Update(TEntity entity);
        TEntity UpdateAndSave(TEntity entity);
        Task<TEntity> UpdateAndSaveAsync(TEntity entity);

        void Delete(int id);
        void DeleteAndSave(int id);
        Task DeleteAndSaveAsync(int id);

        void Delete(TEntity entity);
        void DeleteAndSave(TEntity entity);
        Task DeleteAndSaveAsync(TEntity entity);

        TEntity Find(int id);
        Task<TEntity> FindAsync(int id);

        IQueryable<TEntity> AsQueryable();
    }
}
