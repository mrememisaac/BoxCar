using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxCar.Admin.Core.Exceptions
{
    public class DefaultException : Exception
    {
        public DefaultException(string message) : base(message)
        {

        }
    }
}
