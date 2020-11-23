using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.Response;
using WebAPI.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class dt_adsController : ControllerBase
    {

        private readonly DBContext _context;

        public dt_adsController(DBContext context)
        {
            _context = context;
        }

        [HttpGet("{draw_id}")]
        public AdResponse GetByID(int ad_id)
        {
            AdResponse retval = new AdResponse();

            try
            {
                retval.dt_ad = _context.dt_ads.Where(x => x.id == ad_id).FirstOrDefault();
            }
            catch (Exception exc)
            {
                retval.SetError($"Failed to get ad by id {ad_id}.", exc);
            }

            return retval;
        }

        [HttpGet()]
        public AdResponse getAll()
        {
            AdResponse retval = new AdResponse();

            try
            {
                retval.dt_ads = _context.dt_ads.ToList();
            }
            catch (Exception exc)
            {
                retval.SetError($"Failed to get all ads", exc);
            }

            return retval;
        }

        [HttpPost()]
        public AdResponse create(dt_ads ad)
        {
            AdResponse retval = new AdResponse();

            try
            {
                _context.dt_ads.Add(ad);
                _context.SaveChanges();
                retval.dt_ad = ad;
            }
            catch (Exception exc)
            {
                retval.SetError($"Failed to create ad", exc);
            }

            return retval;
        }

        [HttpPut()]
        public AdResponse update(dt_ads ad)
        {
            AdResponse retval = new AdResponse();

            try
            {
                _context.dt_ads.Update(ad);
                _context.SaveChanges();
                retval.dt_ad = ad;
            }
            catch (Exception exc)
            {
                retval.SetError($"Failed to update ad", exc);
            }

            return retval;
        }

        [HttpDelete()]
        public bool Delete(int id)
        {
            AdResponse retval = new AdResponse();

            try
            {
                var deleteItem = _context.dt_ads.Where(x => x.id == id).FirstOrDefault();
                _context.Entry(deleteItem).State = EntityState.Deleted;
                _context.SaveChanges();
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }

        }



    }
}
