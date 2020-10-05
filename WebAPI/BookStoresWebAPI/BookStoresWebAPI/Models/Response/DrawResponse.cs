using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.Response;

namespace WebApi.Models.Response
{
    public class DrawResponse : Base
    {
        public dt_draw dt_draw { get; set; }
        public List<dt_draw> dt_draws { get; set; }
    }
}
