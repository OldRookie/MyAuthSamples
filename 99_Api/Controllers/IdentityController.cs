using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace _99_Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class IdentityController: ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [HttpGet("ReadData")]
        public IActionResult ReadData()
        {
            bool userHasRightScope = User.HasClaim("scope", "read");
            if (userHasRightScope == false)
            {
                return Unauthorized();
            }

            return Ok("Esta es la info");
        }

        [HttpGet("ReadEnhancedData")]
        public IActionResult ReadEnhancedData()
        {
            bool userHasRightScope = User.HasClaim("scope", "readEnhanced");
            if (userHasRightScope == false)
            {
                return Unauthorized();
            }

            return Ok("Esta es la info avanzada");
        }

    }
}
