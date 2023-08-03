using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_WebApp.Domain.Exceptions
{
    public class ApiException  : Exception
    {
        public ApiException() : base() { }

        public ApiException(string message) : base(message) { }
        public ApiException(HttpStatusCode StatusCode, string message) : base(message)
        {
            this.StatusCode = (int)StatusCode;
        }


        public int StatusCode { get; set; }
    }
}
