using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.Response;

namespace WebApi.Models.Response
{
    public class ConfigResponse : Base
    {
        public dt_config dt_config { get; set; }
    }
}
