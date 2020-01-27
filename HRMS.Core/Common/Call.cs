using System;
using System.Collections.Generic;
using System.Text;

namespace HRMS.Core.Common
{
    public class Call<T> : Call
    {
        public Call()
        {
        }
        public T Result { get; set; }
    }

    public class Call
    {
        public Call()
        {
        }
        public bool IsSuccessful { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }
}
