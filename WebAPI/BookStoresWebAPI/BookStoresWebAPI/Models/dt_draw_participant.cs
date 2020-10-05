using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Table("dt_draw_participant")]
    public class dt_draw_participant
    {
        [Key]
        public int id { get; set; }
        public int draw_id { get; set; }
        public DateTime issue_timestamp { get; set; }
        public string reference { get; set; }
        public string display_name { get; set; }
        public int tickets { get; set; }
        public bool cancelled { get; set; }
        public int issued_by { get; set; }
    }
}
