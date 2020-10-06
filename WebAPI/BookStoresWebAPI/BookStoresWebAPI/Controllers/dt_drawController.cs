using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Response;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class dt_drawController : ControllerBase
    {
        private readonly DBContext _context;

        public dt_drawController(DBContext context)
        {
            _context = context;
        }

        [HttpGet("CanPlay")]
        public bool CanPlay(int draw_id)
        {
            dt_draw draw = _context.dt_draw.Where(x => x.id == draw_id).FirstOrDefault();
            DateTime timeNow = DateTime.Now;

            if (timeNow >= draw.draw_time)
            {
                return true;
            }

            return false;
        }

        [HttpGet]
        public List<dt_draw> Get()
        {
            var draws = _context.dt_draw.ToList();

            return draws;
        }

        [HttpGet("{draw_id}")]
        public DrawResponse GetByDrawID(int draw_id)
        {
            DrawResponse retval = new DrawResponse();

            try
            {
                retval.dt_draw = _context.dt_draw.Find(draw_id);
                retval.dt_draw.prizes = _context.dt_draw_prize.Where(x => x.draw_id == draw_id).ToList();
            }
            catch (Exception exc)
            {
                retval.SetError($"Failed to get draws.", exc);
            }

            return retval;
        }

        [HttpPost]
        public DrawResponse Create(dt_draw dt_draw)
        {
            DrawResponse retval = new DrawResponse();

            try
            {
                _context.dt_draw.Add(dt_draw);
                _context.SaveChanges();
                retval.dt_draw = dt_draw;
            }
            catch (Exception exc)
            {
                retval.SetError($"Failed to create draw.", exc);
            }

            return retval;
        }

        [HttpPut]
        public DrawResponse Update(dt_draw dt_draw)
        {
            DrawResponse retval = new DrawResponse();

            try
            {
                _context.Entry(dt_draw).State = EntityState.Modified;
                _context.SaveChanges();

              var existingPrizes = _context.dt_draw_prize.Where(x=> x.draw_id == dt_draw.id).ToList();
                if (existingPrizes!=null) {
                    foreach (var item in existingPrizes)
                    {
                        _context.Entry(item).State = EntityState.Deleted;
                        _context.SaveChanges();
                    }
                }
                if (dt_draw.prizes!=null) {
                    foreach (var item in dt_draw.prizes)
                    {
                      var newPrize =   _context.dt_draw_prize.Add(item);
                        _context.SaveChanges();
                    }
                }
               
            }
            catch (Exception exc)
            {
                retval.SetError($"Failed to update draw.", exc);
            }

            return retval;
        }

        [HttpDelete]
        public DrawResponse Delete(int draw_id)
        {
            DrawResponse retval = new DrawResponse();

            try
            {
                dt_draw dt_draw = _context.dt_draw.Find(draw_id);

                if (dt_draw == null)
                {
                    retval.SetError($"Failed to delete draw. Could not find draw by id {draw_id}.");
                }
                else
                {
                    _context.Entry(dt_draw).State = EntityState.Deleted;
                    _context.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                retval.SetError($"Failed to update draw.", exc);
            }

            return retval;
        }
    }
}
