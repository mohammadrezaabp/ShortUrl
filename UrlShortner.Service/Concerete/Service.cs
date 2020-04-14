using Mapster;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UrlShortner.Data.Entities;
using UrlShortner.Repository.Abstract;
using UrlShortner.Service.Abstract;
using UrlShortner.Service.Models;

namespace UrlShortner.Service.Concerete
{
    public class Service<TInput, TResult> : IService<TInput, TResult>

        where TResult : BaseServiceModel
        where TInput : BaseEntity
    {
        private readonly IRepository<TInput> _repository;

        public Service(IRepository<TInput> repository)
        {
            _repository = repository;
          
        }


       



        public virtual void Insert(TResult serviceModel)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<TResult, TInput>().MaxDepth(1);
            _repository.Insert(serviceModel.Adapt<TInput>(config));
           
        }
       

       

        public virtual void Update(TResult serviceModel)
        {
            var props = typeof(TResult).GetProperties().Where(p => p.GetGetMethod().IsVirtual).Select(p => p.Name).ToArray();
            TypeAdapterConfig<TResult, TInput>.NewConfig()
                .Ignore(props)
                .Ignore(p => p.AddedDate)
               
                .Map(p => p.ModifiedDate, s => DateTime.Now)
               

                ;
            TypeAdapterConfig<TInput, TInput>.NewConfig()
                .Ignore(props)
                .Ignore(p => p.AddedDate)


                .Map(p => p.ModifiedDate, s => DateTime.Now);


            var model = _repository.Get(serviceModel.Id);
            var mapedEntity = serviceModel.Adapt(model);

            var m = mapedEntity.Adapt<TInput>();
            _repository.Update(m);
           


        }

       

        public IEnumerable<TResult> GetBy(Expression<Func<TResult, bool>> predicate)
        {
            var ff = _repository.GetBy(predicate.ToReplaceParameter<TResult, TInput>())
                .AsEnumerable()
                .Adapt<IEnumerable<TResult>>();
            return ff;

        }
       
    }
    public static class ExpressionExtensions
    {
        public static Expression<Func<TTo, bool>> ToReplaceParameter<TFrom, TTo>(this Expression<Func<TFrom, bool>> target)
        {
            return (Expression<Func<TTo, bool>>)new WhereReplacerVisitor<TFrom, TTo>().Visit(target);
        }
        private class WhereReplacerVisitor<TFrom, TTo> : ExpressionVisitor
        {
            private readonly ParameterExpression _parameter = Expression.Parameter(typeof(TTo), "c");

            protected override Expression VisitLambda<T>(Expression<T> node)
            {
                // replace parameter here
                return Expression.Lambda(Visit(node.Body), _parameter);
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                // replace parameter member access with new type
                if (node.Member.DeclaringType == typeof(TFrom) && node.Expression is ParameterExpression)
                {
                    return Expression.PropertyOrField(_parameter, node.Member.Name);
                }
                return base.VisitMember(node);
            }
        }
    }
}
