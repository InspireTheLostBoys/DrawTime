using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class dt_draw_participant
    {
        public int id { get; set; }
        public string draw_id { get; set; }
        public string issue_timestamp { get; set; }
        public string reference { get; set; }
        public string display_name { get; set; }
        public string tickets { get; set; }
        public string cancelled { get; set; }
        public string issued_by { get; set; }
    }
}
