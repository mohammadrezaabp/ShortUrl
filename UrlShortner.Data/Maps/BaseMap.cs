using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UrlShortner.Data.Entities;

namespace UrlShortner.Data.Maps
{
    public class BaseMap<T> where T : BaseEntity
    {
        public BaseMap(EntityTypeBuilder<T> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
        }
    }
}
