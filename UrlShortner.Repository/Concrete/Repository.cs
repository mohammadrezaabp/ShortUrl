using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UrlShortner.Data.Entities;
using UrlShortner.Repository.Abstract;

namespace UrlShortner.Repository.Concrete
{
    public class Repository<T> : IRepository<T> where T : BaseEntity

    {
       
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _entity;
        string errorMessage = string.Empty;

        public Repository(ApplicationContext context)
        {
            this._context = context;
           
            _entity = context.Set<T>();
        }

       

        public void Insert(T entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entity.AddedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
           


            this._entity.Add(entity);
            _context.SaveChanges();
            

        }

        
        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                entity.ModifiedDate = DateTime.Now;
               

                _context.SaveChanges();

               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        
        public void SaveChanges()
        {
            _context.SaveChanges();
        }


       
        public virtual IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return _entity.Where(predicate);
        }

        public T Get(long id)
        {
            return _entity.SingleOrDefault(s => s.Id == id);
        }
    }
}
