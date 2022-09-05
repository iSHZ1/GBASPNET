using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using MetricsAgent.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IHddMetricsRepository _hddMetricsRepository;

        public HddMetricsController(
            IHddMetricsRepository hddMetricsRepository,
            ILogger<HddMetricsController> logger)
        {
            _hddMetricsRepository = hddMetricsRepository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _hddMetricsRepository.Create(new HddMetric
            {
                Value = request.Value,
                Time = (int)request.Time.TotalSeconds
            });
            return Ok();
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<HddMetric>> GetHddMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get hdd metrics call.");
            return Ok(_hddMetricsRepository.GetByTimePeriod(fromTime, toTime));
        }


    }
}
