using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxCar.Admin.Core.Contracts.Identity
{
    public interface ILoggedInUserService
    {
        public string UserId { get; }
    }
}
