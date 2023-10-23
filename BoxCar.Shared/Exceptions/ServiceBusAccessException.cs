using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BoxCar.Shared.Exceptions
{
    public class ServiceBusAccessException : Exception
    {
        public EventId EventId = new EventId(1, nameof(ServiceBusAccessException));

        public HttpStatusCode StatusCode = HttpStatusCode.InternalServerError;
        
    }
}
