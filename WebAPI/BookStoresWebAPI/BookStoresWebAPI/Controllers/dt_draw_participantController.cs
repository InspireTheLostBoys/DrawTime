﻿using System;
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
    public class dt_draw_participantController : ControllerBase
    {
        private readonly DBContext _context;

        public dt_draw_participantController(DBContext context)
        {
            _context = context;
        }

        [HttpGet("{draw_id}")]
        public ParticipantResponse GetByDrawID(int draw_id)
        {
            ParticipantResponse retval = new ParticipantResponse();

            try
            {
                retval.dt_draw_participants = _context.dt_draw_participant.Where(x => x.draw_id == draw_id).ToList();
            }
            catch(Exception exc)
            {
                retval.SetError($"Failed to get participants by draw id {draw_id}.", exc);
            }

            return retval;
        }

        [HttpPost]
        public ParticipantResponse Create(dt_draw_participant dt_draw_participant)
        {
            ParticipantResponse retval = new ParticipantResponse();

            try
            {
                _context.dt_draw_participant.Add(dt_draw_participant);
                _context.SaveChanges();
                retval.dt_draw_participant = dt_draw_participant;
            }
            catch (Exception exc)
            {
                retval.SetError($"Failed to create participant.", exc);
            }

            return retval;
        }

        [HttpPut]
        public ParticipantResponse Update(dt_draw_participant dt_draw_participant)
        {
            ParticipantResponse retval = new ParticipantResponse();

            try
            {
                _context.Entry(dt_draw_participant).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception exc)
            {
                retval.SetError($"Failed to update participant.", exc);
            }

            return retval;
        }

        [HttpDelete]
        public ParticipantResponse Delete(int participant_id)
        {
            ParticipantResponse retval = new ParticipantResponse();

            try
            {
                dt_draw_participant dt_draw_participant = _context.dt_draw_participant.Find(participant_id);

                if(dt_draw_participant == null)
                {
                    retval.SetError($"Failed to delete participant. Could not find participant with id {participant_id}");
                }
                else
                {
                    _context.Entry(dt_draw_participant).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    _context.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                retval.SetError($"Failed to update participant.", exc);
            }

            return retval;
        }
    }
}