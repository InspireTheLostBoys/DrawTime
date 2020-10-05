using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class dt_draw
    {
        public int id { get; set; }
        public string draw_name { get; set; }
        public string issue_open { get; set; }
        public string issue_close { get; set; }
        public string draw_time { get; set; }
        public string draw_type { get; set; }
        public string entry_fee { get; set; }
        public string ticket_cost { get; set; }
        public string cancelled { get; set; }
        public string completed { get; set; }
    }
}
