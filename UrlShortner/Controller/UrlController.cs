using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Service.Abstract;

namespace UrlShortner
{


    public class UrlController : Controller
    {

        private readonly IShortUrlService _shortUrlService;
        public UrlController(IShortUrlService shortUrlService)

           
        {
            _shortUrlService = shortUrlService;
        }

        [HttpPost]
        public IActionResult CreateShortCode([FromBody] ShortUrlModel model)
        {

            var token = _shortUrlService.CreateShortCode(model.siteurl);
            return Ok(token);
        }
        
        [HttpGet]
        public IActionResult GetByCode(string code)
        {
            var model = _shortUrlService.GetBy(r => r.Token == code).FirstOrDefault();
            if (model != null)
            {
                model.Count = model.Count + 1;
                _shortUrlService.Update(model);
                return Ok(model);
            }
            return BadRequest();

        }

    }
    public class ShortUrlModel {
        public string siteurl { get; set; } 
    } 
}