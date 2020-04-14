using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UrlShortner.Data.Entities;
using UrlShortner.Repository.Abstract;
using UrlShortner.Service.Abstract;
using UrlShortner.Service.Models;

namespace UrlShortner.Service.Concerete
{
    public class ShortUrlService : Service<ShortUrl, ShortUrlServiceModel>, IShortUrlService
    {
        private readonly IShortUrlRepository _ShortUrlRepository;
        public ShortUrlService(IShortUrlRepository ShortUrlRepository) : base(ShortUrlRepository)
        {
            this._ShortUrlRepository = ShortUrlRepository;
        }

        public string CreateShortCode(string siteUrl)
        {
            var shortUrl = new ShortUrlServiceModel();
            shortUrl.SiteUrl = siteUrl;
            shortUrl.Token = RandomString(6);
            shortUrl.ShortenedUrl = "url/" + shortUrl.Token;
            base.Insert(shortUrl);
            return (shortUrl.Token);
        }
        private string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
