using Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.BaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        MobDbContext GetContext();
        IQueryable<T> GetAll();
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
        T GetById(int id);
        T Create(T entity);
        void Update(T entity);
        void Delete(int id);
        void SaveChanges();
    }
}
