using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.Response;

namespace WebApi.Models.Response
{
    public class ParticipantResponse : Base
    {
        public dt_draw_participant dt_draw_participant { get; set; }
        public List<dt_draw_participant> dt_draw_participants { get; set; }
    }
}
