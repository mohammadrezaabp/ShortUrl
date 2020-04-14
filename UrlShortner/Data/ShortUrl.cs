using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortner.Data
{
    public class ShortUrl
    {
        [Key]
        public int Id { get; set; }
        public string SiteUrl { get; set; }
        public string ShortenedUrl { get; set; }
        public int Count { get; set; }

    }
}
