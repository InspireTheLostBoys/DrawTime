using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Table("dt_config")]
    public class dt_config
    {
        [Key]
        public int id { get; set; }
        public string venue_name { get; set; }
        public string t_and_c { get; set; }
    }
}
