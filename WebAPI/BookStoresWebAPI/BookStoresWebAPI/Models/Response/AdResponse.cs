using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.Response;

namespace WebApi.Models.Response
{
    public class AdResponse : Base
    {
        public dt_ads dt_ad { get; set; }
        public List<dt_ads> dt_ads { get; set; }
    }
}
