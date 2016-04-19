using System.Linq;
using System.Threading.Tasks;
using TripPlaner.DAL;
using TripPlaner.DAL.Entities;
using TripPlaner.DAL.Repositories;

namespace TripPlaner.Services.Services
{
    public class EntityService<TEntity> : IEntityService<TEntity> where TEntity : class, IEntity
    {
        protected IUnitOfWork UnitOfWork;

        protected IEntityRepository<TEntity> Repository;

        public EntityService(IUnitOfWork unitOfWork, IEntityRepository<TEntity> repository)
        {
            UnitOfWork = unitOfWork;
            Repository = repository;
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return Repository.Insert(entity);
        }

        public virtual TEntity InsertAndSave(TEntity entity)
        {
            var result = Repository.Insert(entity);
            UnitOfWork.SaveChange();
            return result;
        }

        public virtual async Task<TEntity> InsertAndSaveAsync(TEntity entity)
        {
            var result = Repository.Insert(entity);
            await UnitOfWork.SaveChangeAsync();
            return result;
        }

        public virtual TEntity Update(TEntity entity)
        {
            return Repository.Update(entity);
        }

        public virtual TEntity UpdateAndSave(TEntity entity)
        {
            var result = Repository.Update(entity);
            UnitOfWork.SaveChange();
            return result;
        }

        public virtual async Task<TEntity> UpdateAndSaveAsync(TEntity entity)
        {
            var result = Repository.Update(entity);
            await UnitOfWork.SaveChangeAsync();
            return result;
        }

        public virtual void Delete(int id)
        {
            Repository.Delete(id);
        }

        public virtual void DeleteAndSave(int id)
        {
            Repository.Delete(id);
            UnitOfWork.SaveChange();
        }

        public virtual async Task DeleteAndSaveAsync(int id)
        {
            Repository.Delete(id);
            await UnitOfWork.SaveChangeAsync();
        }

        public virtual void Delete(TEntity entity)
        {
            Repository.Delete(entity);
        }

        public virtual void DeleteAndSave(TEntity entity)
        {
            Repository.Delete(entity);
            UnitOfWork.SaveChange();
        }

        public virtual async Task DeleteAndSaveAsync(TEntity entity)
        {
            Repository.Delete(entity);
            await UnitOfWork.SaveChangeAsync();
        }

        public virtual TEntity Find(int id)
        {
            return Repository.Find(id);
        }

        public virtual async Task<TEntity> FindAsync(int id)
        {
            return await Repository.FindAsync(id);
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return Repository.AsQueryable();
        }
    }
}
