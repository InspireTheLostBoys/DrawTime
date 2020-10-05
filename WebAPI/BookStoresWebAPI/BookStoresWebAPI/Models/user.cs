using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Table("user")]
    public class user
    {
        [Key]
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string cms_id { get; set; }
        public string uid { get; set; }
        public string pwd { get; set; }
        public string pin { get; set; }
        public bool inactive { get; set; }
    }
}
