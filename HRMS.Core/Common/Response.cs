using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Core.Common
{
    public class Response<T> : Response
    {
        public Response()
        {
        }
        public T Result { get; set; }
    }

    public class Response
    {
        public Response()
        {
        }
        public bool IsSuccessful { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }
}
