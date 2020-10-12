using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Table("dt_draw")]
    public class dt_draw
    {
        [Key]
        public int id { get; set; }
        public string draw_name { get; set; }
        public DateTime issue_open { get; set; }
        public DateTime issue_close { get; set; }
        public DateTime draw_time { get; set; }
        public int draw_type { get; set; }
        public bool entry_fee { get; set; }
        public decimal ticket_cost { get; set; }
        public bool cancelled { get; set; }
        public bool completed { get; set; }
        public int total_tickets { get; set; }
        public decimal total_pot { get; set; }

        [NotMapped]
        public List<dt_draw_prize> prizes { get; set; }
       
    }
}
