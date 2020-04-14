using System;
using System.Collections.Generic;
using System.Text;

namespace UrlShortner.Service.Models
{
    public class BaseServiceModel
    {
        public long Id { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
