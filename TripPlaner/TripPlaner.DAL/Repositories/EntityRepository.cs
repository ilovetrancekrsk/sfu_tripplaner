using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public TEntity Insert(TEntity entity)
        {
            return Set.Add(entity);
        }

        public TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            DbContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TEntity Find(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            throw new NotImplementedException();
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
