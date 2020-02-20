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
        public string Message
        {
            get
            {
                if (Exception != null && Exception is HRMSException)
                {
                    return Exception.Message;
                }
                else if (Exception != null)
                {
                    return "An error has happened,please contact administrator";
                }
                return "";
            }
        }
    }
}
