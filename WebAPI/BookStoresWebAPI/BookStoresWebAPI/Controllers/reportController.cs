using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebApi.Models.Response;
using WebAPI.Models;
using Dapper;

namespace WebApi.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class reportController : ControllerBase
    {

        private IConfiguration configuration;
        private readonly DBContext _context;

        public reportController(DBContext context, IConfiguration iConfig)
        {
            _context = context;
            configuration = iConfig;
        }

        [HttpPost("Period")]
        public object getByDateRange(DateTime period_start, DateTime period_end)
        {


            try
            {
                string dbConn = configuration.GetSection("ConnectionStrings").GetSection("BIAB").Value;

                using (var db = new Npgsql.NpgsqlConnection(dbConn))
                {

                    var query = @"
                                 select
                                dt_draw.draw_name,
 dt_draw.id as draw_id,

                                dt_draw.ticket_cost,
                                sum(dt_draw_participant.tickets)
                                from dt_draw
                                inner join dt_draw_participant on dt_draw.id = dt_draw_participant.draw_ID
                                inner
                                join public.user on public.user.id = dt_draw_participant.issued_by
                                where dt_draw.draw_time BETWEEN '" + period_start + "' AND '" + period_end + @"'  
                                group by dt_draw.draw_name,dt_draw.ticket_cost,dt_draw.id";
                    return db.Query<object>(query);
                }
            }
            catch (Exception exc)
            {
                return (exc);
            }

        }


        [HttpPost("Tickets")]
        public object GetTicketSales(string period_start, string period_end)
        {
            try
            {
                string dbConn = configuration.GetSection("ConnectionStrings").GetSection("BIAB").Value;

                using (var db = new Npgsql.NpgsqlConnection(dbConn))
                {

                    var query = @"
                                select 
                                concat(public.user.firstname , ' ' ,public.user.lastname) as username,
                                dt_draw.draw_name,
 dt_draw.id as draw_id,
                                dt_draw.ticket_cost,
                                sum(dt_draw_participant.tickets)
                                from dt_draw 
                                inner join dt_draw_participant on dt_draw.id= dt_draw_participant.draw_ID 
                                inner join public.user on public.user.id = dt_draw_participant.issued_by 
                                where dt_draw.draw_time BETWEEN '" + period_start + "' AND '" + period_end + @"' 
                                group by dt_draw.draw_name,dt_draw.ticket_cost, username,dt_draw.id";
                    return db.Query<object>(query);
                }
            }
            catch (Exception exc)
            {

                return exc;
            }
        }

        [HttpPost("Reseller")]
        public object getResellerReport(string period_start, string period_end)
        {
            try
            {
                string dbConn = configuration.GetSection("ConnectionStrings").GetSection("BIAB").Value;

                using (var db = new Npgsql.NpgsqlConnection(dbConn))
                {

                    var query = @"
                                   select
								 dt_draw.id as draw_id,
                                dt_draw.draw_name,
                                dt_draw.ticket_cost,
                                sum(dt_draw_participant.tickets),
                                dt_draw_prize.prize_cost,
                                dt_draw_prize.description,
                                dt_draw_prize.prize_value,
                                 dt_draw_prize.pot_percentage
                                from dt_draw
                                inner join dt_draw_participant on dt_draw.id= dt_draw_participant.draw_ID

                                inner join dt_draw_prize on dt_draw.id = dt_draw_prize.draw_id
                               where dt_draw.draw_time BETWEEN '" + period_start + "' AND '" + period_end + @"' 
                                group by dt_draw.draw_name, dt_draw.ticket_cost, dt_draw_prize.prize_cost, dt_draw_prize.prize_value, dt_draw_prize.pot_percentage,dt_draw_prize.description,dt_draw.id";

                    return db.Query<object>(query);
                }
            }
            catch (Exception exc)
            {

                return exc;
            }
        }

        [HttpPost("draw")]
        public object getDrawReport(string period_start, string period_end, int draw_id)
        {
            try
            {
                string dbConn = configuration.GetSection("ConnectionStrings").GetSection("BIAB").Value;
                using (var db = new Npgsql.NpgsqlConnection(dbConn))
                {

                    var query = @"
                                   
                                 select
								 dt_draw.id as draw_id,
                                dt_draw.draw_name,
                                dt_draw.ticket_cost,
                                sum(dt_draw_participant.tickets),
								dt_draw_participant.display_name,
								dt_draw_participant.reference,
								dt_draw_prize.description,
								dt_draw_prize.prize_cost,
								dt_draw_prize.prize_value
                                from dt_draw
                                inner join dt_draw_participant on dt_draw.id = dt_draw_participant.draw_ID
                                inner
                                join public.user on public.user.id = dt_draw_participant.issued_by
								full outer join dt_draw_prize on dt_draw_participant.id = dt_draw_prize.winner_id 
                                  where dt_draw.draw_time BETWEEN '" + period_start + "' AND '" + period_end + @"'  and dt_draw.id =  " + draw_id + @"
                                group by dt_draw.draw_name,dt_draw.ticket_cost,dt_draw.id,
								dt_draw_participant.display_name,dt_draw_participant.reference,dt_draw_prize.description,dt_draw_prize.prize_cost,
								dt_draw_prize.prize_value
								";

                    return db.Query<object>(query);
                }
            }
            catch (Exception exc)
            {

                return exc;
            }
        }

    }
}
