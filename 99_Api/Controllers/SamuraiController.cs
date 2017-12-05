using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace _99_Api.Controllers
{
    [Route("api/[controller]")]
    public class SamuraiController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Takeshi", "Kojima" };
        }
    }
}
