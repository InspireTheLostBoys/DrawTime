using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class dt_draw_prize
    {
        public int id { get; set; }
        public int draw_id { get; set; }
        public int description { get; set; }
        public int sequence_no { get; set; }
        public int show_value { get; set; }
        public int percentage_of_pot { get; set; }
        public int pot_percentage { get; set; }
        public int prize_value { get; set; }
        public int winner_id { get; set; }
    }
}
