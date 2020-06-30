using Microsoft.EntityFrameworkCore;
using Models.Context;
using Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BL.Services.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly MobDbContext _context;
        private DbSet<T> _dbSet;

        public BaseRepository(MobDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).AsNoTracking();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T Create(T entity)
        {
            _dbSet.Add(entity);
            SaveChanges();
            return entity;
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            SaveChanges();
        }

        public void Delete(int id)
        {
            var exist = _dbSet.Find(id);
            _dbSet.Remove(exist);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public MobDbContext GetContext()
        {
            return _context;
        }
    }
}
