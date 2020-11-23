using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Table("dt_draw_prize")]
    public class dt_draw_prize
    {
        [Key]
        public int id { get; set; }
        public int draw_id { get; set; }
        public string description { get; set; }
        public int sequence_no { get; set; }
        public bool show_value { get; set; }
        public bool percentage_of_pot { get; set; }
        public decimal pot_percentage { get; set; }
        public decimal prize_value { get; set; }
        public int winner_id { get; set; }
        public string prize_image { get; set; }



    }
}
