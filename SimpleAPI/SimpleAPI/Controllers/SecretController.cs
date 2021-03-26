using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SimpleAPI.Controllers
{
    // Needed to enforce authentication at class level
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("text/plain")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecretController : ControllerBase
    {
        // GET: secret
        [HttpGet]
        public IActionResult Index()
        {
            var res = $"Authorized request successful for user ID: {User.Identity?.Name}";
            return new OkObjectResult(res);
        }

        // GET: secret/userdetails
        [HttpGet("userdetails")]
        public IActionResult UserDetails()
        {
            var res = User.Claims.Aggregate("UserDetails Request successful\n\nClaims:\n", (current, userClaim) => current + $"{userClaim.Type}: {userClaim.Value}\n");

            return new OkObjectResult(res);
        }

        // GET: secret/admin
        [HttpGet("admin")]
        // Restrict to users with admin role
        [Authorize(Roles = "admin")]
        public IActionResult Admin()
        {
            const string res = "Admin endpoint authorized";

            return new OkObjectResult(res);
        }

    }
}
