using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TripPlaner.DAL.Entities;

namespace TripPlaner.DAL.Repositories
{
    public class EntityRepository<TEntity> 
        : IEntityRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly IUnitOfWork _unitOfWork;

        protected TripPlanerDbContext DbContext
        {
            get { return _unitOfWork.DbContext; }
        }

        protected DbSet<TEntity> Set
        {
            get { return _unitOfWork.DbContext.Set<TEntity>(); }
        } 

        public EntityRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return Set.Add(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            DbContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public virtual void Delete(int id)
        {
            var entity = Set.Find(id);
            if (entity == null)
                return;
            
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Set.Remove(entity);
        }

        public virtual TEntity Find(int id)
        {
            return Set.Find(id);
        }

        public virtual async Task<TEntity> FindAsync(int id)
        {
            return await Set.FindAsync(id);
        }

        public virtual IQueryable<TEntity> AsQueryable()
        {
            return Set.AsQueryable();
        }

        /// <summary>
        /// Добавляем в контекст репозитория если небыло
        /// </summary>
        /// <param name="entity"></param>
        private void AttachIfNot(TEntity entity)
        {
            if (!Set.Local.Contains(entity))
            {
                Set.Attach(entity);
            }
        }
    }
}
