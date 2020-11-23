using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.Response;

namespace WebApi.Models.Response
{
    public class PrizeResponse : Base
    {
        public dt_draw_prize dt_draw_prize { get; set; }
    }
}
