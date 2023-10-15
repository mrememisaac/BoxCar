using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxCar.Catalogue.Core.Contracts.Identity
{
    public interface ILoggedInUserService
    {
        public string UserId { get; }
    }
}
