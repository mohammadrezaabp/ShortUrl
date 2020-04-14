using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Data;

namespace UrlShortner.Controller
{


    public class UrlController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ShortUrl> _entity;
        public UrlController(ApplicationDbContext context)
        {
            this._context = context;
            _entity = context.Set<ShortUrl>();

        }

        [HttpPost]
        public IActionResult CreateShortCode (string siteUrl)
        {
            
            var shortUrl = new ShortUrl
            {
                SiteUrl = siteUrl,
                ShortenedUrl = RandomString(5)
            };
            _entity.Add(shortUrl);
            return Ok(shortUrl.ShortenedUrl);
        }
        private string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        [HttpGet]

        public IActionResult GetBy(string code)
        {
            var model = _entity.FirstOrDefault(r => r.ShortenedUrl == code);
            if (model != null)
            {
                model.Count = model.Count + 1;
                return Ok(model);
            }
            return BadRequest();

        }
    }
}