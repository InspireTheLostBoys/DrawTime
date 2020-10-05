using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Response;
using WebAPI.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class dt_configController : ControllerBase
    {
        private readonly DBContext _context;

        public dt_configController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ConfigResponse GetConfig()
        {
            ConfigResponse retval = new ConfigResponse();

            try
            {
                dt_config dt_config = _context.dt_config.ToList().FirstOrDefault();

                if(dt_config == null)
                {
                    dt_config = new dt_config()
                    {
                        venue_name = "",
                        t_and_c = ""
                    };

                    _context.dt_config.Add(dt_config);
                    _context.SaveChanges();
                }

                retval.dt_config = dt_config;
            }
            catch(Exception exc)
            {
                retval.SetError($"Failed to get config.", exc);
            }

            return retval;
        }

        [HttpPut]
        public ConfigResponse UpdateConfig(dt_config dt_config)
        {
            ConfigResponse retval = new ConfigResponse();

            try
            {
                _context.Entry(dt_config).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception exc)
            {
                retval.SetError($"Failed to update config.", exc);
            }

            return retval;
        }
    }
}
