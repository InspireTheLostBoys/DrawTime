using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Request
{
    public class LoginRequest
    {
        public string uid { get; set; }
        public string pin { get; set; }
    }
}
