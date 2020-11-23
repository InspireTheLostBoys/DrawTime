using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Models
{
    [Table("dt_ads")]

    public class dt_ads
    {
        [Key]
        public int id { get; set; }
        public string? description { get; set; }
        public string? line1 { get; set; }
        public string? line2 { get; set; }
        public string? line3 { get; set; }
        public string? line4 { get; set; }
        public string? ad_image { get; set; }
        public DateTime? show_from { get; set; }
        public DateTime? show_to { get; set; }
    }
}
