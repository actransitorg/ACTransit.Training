using System;

namespace ACTransit.Training.Web.Business.Infrastructure
{
    public class BusinessException:Exception
    {
        public BusinessException(string message) : base(message)
        {
        }
    }
}
