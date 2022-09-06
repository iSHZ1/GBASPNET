using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMerticsController : ControllerBase
    {
        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetRamMetrics(
            [FromRoute] TimeSpan fromTime, 
            [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}