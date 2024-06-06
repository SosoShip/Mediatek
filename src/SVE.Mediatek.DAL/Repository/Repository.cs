using Microsoft.EntityFrameworkCore;
using SVE.Mediatek.Dal;
using SVE.Mediatek.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.DAL.Repository
{
    /// <summary>
    /// Entity managment from DataBase
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        // DbContext instance for database session
        protected readonly MediatekContext Context;

        // Constructor that initializes the DbContext instance.
        public Repository(MediatekContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Retrieves a entity from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>entity</returns>
        public async Task<TEntity> Get(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// Retrieves all entities from the database.
        /// </summary>
        /// <returns>Queryable of all entities</returns>
        public IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().AsQueryable<TEntity>();
        }

        /// <summary>
        /// Adds a new entity to the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Add(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing entity in the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task Update(TEntity entity)
        {
            //erease context memory
            Context.ChangeTracker.Clear();
            Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an entity from the database by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        { 
            var entity = await Context.Set<TEntity>().FindAsync(id);
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}
