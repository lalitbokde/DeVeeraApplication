using CRM.Core;

using CRM.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly dbContextCRM _dbContext;
        private DbSet<T> _entities;



        public EfRepository(dbContextCRM context)
        {
            this._dbContext = context;
        }

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public virtual T GetById(object id)
        {
            //see some suggested performance optimization (not tested)
            //http://stackoverflow.com/questions/11686225/dbset-find-method-ridiculously-slow-compared-to-singleordefault-on-id/11688189#comment34876113_11688189
            return Entities.Find(id);
        }
        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }
        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.Add(entity);

                _dbContext.SaveChanges();
                _dbContext.Entry<T>(entity).Reload();
            }
            catch (Exception dbEx)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(dbEx.ToString());
            }
        }

        public void Insert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities)
                    Entities.Add(entity);

                _dbContext.SaveChanges();
                _dbContext.Entry<IEnumerable<T>>(Entities).Reload();
            }
            catch (Exception dbEx)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(dbEx.ToString());
            }
        }
        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
                _dbContext.Entry<T>(entity).Reload();
            }
            catch (Exception dbEx)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(dbEx.ToString());
            }
        }
        public void Update(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                _dbContext.SaveChanges();
                _dbContext.Entry<IEnumerable<T>>(entities).Reload();
            }
            catch (Exception dbEx)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(dbEx.ToString());
            }
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                Entities.Remove(entity);

                _dbContext.SaveChanges();
                _dbContext.Entry<T>(entity).Reload();
            }
            catch (Exception dbEx)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(dbEx.ToString());
            }
        }

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public virtual void Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities)
                    Entities.Remove(entity);

                _dbContext.SaveChanges();
                _dbContext.Entry<IEnumerable<T>>(entities).Reload();
            }
            catch (Exception dbEx)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(dbEx.ToString());
            }
        }

   

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _dbContext.Set<T>();
                return _entities;
            }
        }


    }
}
