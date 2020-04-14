using System;
using System.Collections.Generic;
using System.Text;
using UrlShortner.Data.Entities;
using UrlShortner.Service.Models;

namespace UrlShortner.Service.Abstract
{
    public interface IShortUrlService : IService<ShortUrl, ShortUrlServiceModel>
    {
        string CreateShortCode(string siteUrl);
    }
}
