using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UrlShortner.Data.Entities;

namespace UrlShortner.Data.Maps
{
    public class ShortUrlMap : BaseMap<ShortUrl>
    {
        public ShortUrlMap(EntityTypeBuilder<ShortUrl> entityTypeBuilder) : base(entityTypeBuilder)
        {


        }
    }
}
