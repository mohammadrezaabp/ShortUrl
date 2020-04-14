using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using UrlShortner.Data.Entities;
using UrlShortner.Service.Models;

namespace UrlShortner.Service.Abstract
{
    public interface IService
    {

    }
    public interface IService<TInput, TResult>
        where TInput : BaseEntity
        where TResult : BaseServiceModel
    {
        
       
        void Insert(TResult model);
       
        void Update(TResult model);
       

        IEnumerable<TResult> GetBy(Expression<Func<TResult, bool>> predicate);

    }
}
