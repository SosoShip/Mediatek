﻿using SVE.Mediatek.DAL.Entities;

namespace SVE.Mediatek.DAL.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(int id);
        IQueryable<TEntity> GetAll();
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(int id);
    }
}