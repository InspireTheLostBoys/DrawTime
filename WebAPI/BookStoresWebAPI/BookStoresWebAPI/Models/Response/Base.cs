using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Response
{
    public class Base
    {
        public Base()
        {
            this.Success = true;
        }

        public bool Success { get; set; }
        public string ClientError { get; set; }
        public string TechnicalError { get; set; }

        public void SetError(string clientError)
        {
            this.Success = false;
            this.ClientError = clientError;
        }

        public void SetError(string clientError, Exception exc)
        {
            this.Success = false;
            this.ClientError = clientError;
            this.TechnicalError = exc.Message;
        }
    }
}
