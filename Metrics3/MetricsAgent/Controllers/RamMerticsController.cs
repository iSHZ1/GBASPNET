using MetricsAgent.Models.Requests;
using MetricsAgent.Models;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Models.Dto;
using MetricsAgent.Services.Impl;
using AutoMapper;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamMerticsController : ControllerBase
    {
        private readonly ILogger<RamMerticsController> _logger;
        private readonly IRamMetricsRepository _ramMetricsRepository;
        private readonly IMapper _mapper;

        public RamMerticsController(
            IRamMetricsRepository ramMetricsRepository,
            ILogger<RamMerticsController> logger,
            IMapper mapper)
        {
            _ramMetricsRepository = ramMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _ramMetricsRepository.Create(_mapper.Map<RamMetric>(request));
            return Ok();
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<RamMetric>> GetRamMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get Ram metrics call.");

            return Ok(_ramMetricsRepository.GetByTimePeriod(fromTime, toTime)
                .Select(metric => _mapper.Map<RamMetricDto>(metric)).ToList());
        }
    }
}
