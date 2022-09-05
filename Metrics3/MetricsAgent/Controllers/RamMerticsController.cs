using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamMerticsController : ControllerBase
    {
        private readonly ILogger<RamMerticsController> _logger;
        private readonly IRamMetricsRepository _ramMetricsRepository;

        public RamMerticsController(
            IRamMetricsRepository ramMetricsRepository,
            ILogger<RamMerticsController> logger)
        {
            _ramMetricsRepository = ramMetricsRepository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _ramMetricsRepository.Create(new RamMetric
            {
                Value = request.Value,
                Time = (int)request.Time.TotalSeconds
            });
            return Ok();
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<RamMetric>> GetRamMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get Ram metrics call.");
            return Ok(_ramMetricsRepository.GetByTimePeriod(fromTime, toTime));
        }
    }
}
