using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

namespace SimpleAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("text/plain")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecretController : ControllerBase
    {
        // GET: Secret
        [HttpGet]
        public IActionResult Index()
        {
            var userId = GetUserId();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return new UnauthorizedResult();
            }

            var res = "Authorized request successful";
            return new OkObjectResult(res);
        }

        // GET: Secret/User
        [HttpGet("userdetails")]
        public IActionResult UserDetails()
        {
            var userId = GetUserId();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return new UnauthorizedResult();
            }

            var res = User.Claims.Aggregate("UserDetails Request successful\n\nClaims:\n", (current, userClaim) => current + $"{userClaim.Type}: {userClaim.Value}\n");

            return new OkObjectResult(res);
        }

        public string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

    }
}
