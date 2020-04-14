using System;
using System.Collections.Generic;
using System.Text;

namespace UrlShortner.Service.Models
{
    public class ShortUrlServiceModel : BaseServiceModel
    {
        public string SiteUrl { get; set; }
        public string ShortenedUrl { get; set; }
        public string Token { get; set; }
        public int Count { get; set; }
    }
}
