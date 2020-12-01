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
            var draws = _context.dt_draw.Where(x => x.completed == false && x.cancelled == false).OrderBy(x => x.draw_time).ToList();

            return (List<dt_draw>)draws;
        }


        [HttpGet("History")]
        public List<dt_draw> GetHistory()
        {
            var draws = _context.dt_draw.Where(x => x.completed == true || x.cancelled == true).OrderBy(x => x.draw_time).ToList();

            return (List<dt_draw>)draws;
        }

        [HttpPost("Duplicate")]
        public dt_draw DuplicateDraw(dt_draw draw, bool duplicatePrizes, bool duplicateParticipants)
        {
            try
            {
                var draw_id = draw.id;
                draw.id = 0;
                if (draw != null)
                {
                    draw.id = 0;
                    _context.dt_draw.Add(draw);
                    _context.SaveChanges();
                    if (duplicatePrizes)
                    {
                        var existingPrizes = _context.dt_draw_prize.Where(x => x.draw_id == draw_id).ToList();
                        foreach (var item in existingPrizes)
                        {
                            item.id = 0;
                            item.draw_id = draw.id;
                            item.winner_id = 0;
                            _context.dt_draw_prize.Add(item);
                            _context.SaveChanges();
                        }
                    }
                    if (duplicateParticipants)
                    {
                        var existingParticipants = _context.dt_draw_participant.Where(x => x.draw_id == draw_id).ToList();
                        foreach (var item in existingParticipants)
                        {
                            item.id = 0;
                            item.draw_id = draw.id;
                            _context.dt_draw_participant.Add(item);
                            _context.SaveChanges();
                        }
                    }
                }
                return draw;
            }
            catch (Exception exc)
            {
                return null;
            }
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
                dt_draw.total_pot = dt_draw.ticket_cost * dt_draw.total_tickets;
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
                var participants = _context.dt_draw_participant.Where(x => x.draw_id == dt_draw.id).ToList();
                var totaltickets = 0;
                foreach (var participant in participants)
                {
                    totaltickets += participant.tickets;
                }
                dt_draw.total_tickets = totaltickets;
                dt_draw.total_pot = dt_draw.ticket_cost * dt_draw.total_tickets;
                _context.Entry(dt_draw).State = EntityState.Modified;
                _context.SaveChanges();

                var existingPrizes = _context.dt_draw_prize.Where(x => x.draw_id == dt_draw.id).ToList();
                foreach (var item in dt_draw.prizes)
                {
                    if (item.id == 0)
                    {
                        _context.dt_draw_prize.Add(item);
                    }
                }

                foreach (var reqPrize in dt_draw.prizes)
                {
                    var MustDelete = true;
                    var dbPrize = existingPrizes.Where(x => x.id == reqPrize.id).First();
                    if (dbPrize != null)
                    {
                        _context.Entry(dbPrize).State = EntityState.Modified;
                        _context.dt_draw_prize.Update(dbPrize);
                        MustDelete = false;
                        _context.SaveChanges();
                    }
                    if (MustDelete)
                    {
                        _context.Entry(dbPrize).State = EntityState.Deleted;
                    }
                }
                _context.SaveChanges();
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
                    var participatn = _context.dt_draw_participant.Where(x => x.draw_id == draw_id).ToList();
                    var Prizes = _context.dt_draw_prize.Where(x => x.draw_id == draw_id).ToList();
                    foreach (var dbPrize in participatn)
                    {
                        _context.Entry(dbPrize).State = EntityState.Deleted;
                    }
                    foreach (var dbPrize in Prizes)
                    {
                        _context.Entry(dbPrize).State = EntityState.Deleted;
                    }
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
