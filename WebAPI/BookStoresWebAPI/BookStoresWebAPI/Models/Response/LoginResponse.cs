using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Response
{
    public class LoginResponse : Base
    {
        public int user_id { get; set; }
        public int role_id { get; set; }
    }
}
