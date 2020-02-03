using System;

namespace HRMS.Core.Common
{
    public class HRMSException : Exception
    {
        public HRMSException(string message) : base(message)
        {
        }
    }
}
