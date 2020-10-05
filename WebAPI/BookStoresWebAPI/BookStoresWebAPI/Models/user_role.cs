using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Table("user_role")]
    public class user_role
    {
        [Key]
        public int id { get; set; }
        public int user_id { get; set; }
        public int role_id_1 { get; set; }
        public int role_id_2 { get; set; }
        public bool inactive { get; set; }
    }
}
