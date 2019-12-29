﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SAR.Apps.Server.Helpers
{
    public static class UserHelper
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst("UserId");

            return Guid.Parse(claim.Value);
        }

        public static Guid GetPersonId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst("PersonId");

            return Guid.Parse(claim.Value);
        }
    }
}
