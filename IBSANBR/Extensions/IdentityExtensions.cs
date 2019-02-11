using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace IBSANBR.Extensions
{
    public static class IdentityExtensions
    {
        public static int UserId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(CustomClaimTypes.UserId);

            if (claim == null)
                return 0;

            return int.Parse(claim.Value);
        }

        public static string UserName(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(CustomClaimTypes.UserName);
            return claim?.Value ?? string.Empty;
        }

        public static bool IsAdmin(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(CustomClaimTypes.IsAdmin);

            if (claim == null)
                return false;
            return Boolean.Parse(claim.Value);

        }

        public static int PerfilId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(CustomClaimTypes.PerfilId);

            if (claim == null)
                return 0;

            return int.Parse(claim.Value);

        }

        public static string CodigoUnimed(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(CustomClaimTypes.CodigoUnimed);
            return claim?.Value ?? string.Empty;
        }
    }
}
