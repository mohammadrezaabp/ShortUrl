using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UrlShortner.Data.Entities;

namespace UrlShortner.Repository.Abstract
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Get(long id);
        void Insert(T entity);
        void Update(T entity);
        void SaveChanges();
        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
       
    }
}
