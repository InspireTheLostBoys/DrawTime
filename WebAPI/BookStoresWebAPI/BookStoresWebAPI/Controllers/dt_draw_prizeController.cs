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
    public class dt_draw_prizeController : ControllerBase
    {
        private readonly DBContext _context;

        public dt_draw_prizeController(DBContext context)
        {
            _context = context;
        }


        [HttpPost]
        public PrizeResponse Create(dt_draw_prize dt_draw_prize)
        {
            PrizeResponse retval = new PrizeResponse();

            try
            {
                _context.dt_draw_prize.Add(dt_draw_prize);
                _context.SaveChanges();
                retval.dt_draw_prize = dt_draw_prize;
            }
            catch (Exception exc)
            {
                retval.SetError($"Failed to create Prize.", exc);
            }

            return retval;
        }

        [HttpPut]
        public PrizeResponse Modify(dt_draw_prize dt_draw_prize)
        {
            PrizeResponse retval = new PrizeResponse();

            try
            {
                _context.dt_draw_prize.Update(dt_draw_prize);
                _context.SaveChanges();
                retval.dt_draw_prize = dt_draw_prize;
            }
            catch (Exception exc)
            {
                retval.SetError($"Failed to update Prize.", exc);
            }

            return retval;
        }


        [HttpDelete]
        public PrizeResponse delete(int id)
        {
            PrizeResponse retval = new PrizeResponse();

            try
            {
                var deletePrize = _context.dt_draw_prize.Where(x => x.id == id).FirstOrDefault();
                _context.Entry(deletePrize).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
                retval.Success = true;
                return retval;
            }
            catch (Exception exc)
            {
                retval.SetError($"Failed to update Prize.", exc);
            }

            return retval;
        }

    }
}
