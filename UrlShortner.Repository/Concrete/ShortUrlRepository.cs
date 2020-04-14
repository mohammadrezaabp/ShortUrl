using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UrlShortner.Data.Entities;
using UrlShortner.Repository.Abstract;

namespace UrlShortner.Repository.Concrete
{
    public class AnswerRepository : Repository<ShortUrl>, IShortUrlRepository
    {
        private readonly DbSet<ShortUrl> _entity;
        public AnswerRepository(ApplicationContext context) : base(context)
        {
            this._entity = context.Set<ShortUrl>();
        }
    }
}
