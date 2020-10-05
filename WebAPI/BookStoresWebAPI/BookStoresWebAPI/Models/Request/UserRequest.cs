using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Request
{
    public class UserRequest
    {
        public user user { get; set; }
        public int role_id { get; set; }
    }
}
