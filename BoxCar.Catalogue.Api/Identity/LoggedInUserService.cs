﻿using BoxCar.Catalogue.Core.Contracts.Identity;
using System.Security.Claims;

namespace BoxCar.Catalogue.Api.Identity
{
    public class LoggedInUserService : ILoggedInUserService
    {
        public string UserId => GetUserId();

        private readonly IHttpContextAccessor _contextAccessor;

        public LoggedInUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetUserId()
        {
            return _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "emem.isaac";
        }
    }
}
