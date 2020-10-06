using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Response
{
    public class UserResponse : Base
    {
        public user user { get; set; }
        public List<user> users { get; set; }
        public int role_id { get; set; }
    }
}
