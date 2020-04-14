using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UrlShortner.Data.Entities
{
    public class ShortUrl : BaseEntity
    {
       public string SiteUrl { get; set; }
        public string ShortenedUrl { get; set; }
        public int Count { get; set; }
        public string Token { get; set; }

    }
}
