using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Table("role")]
    public class role
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; }
    }
}
