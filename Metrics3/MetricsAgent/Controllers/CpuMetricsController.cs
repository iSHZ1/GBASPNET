﻿using AutoMapper;
using MetricsAgent.Converters;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        private readonly IMapper _mapper;


        public CpuMetricsController(
                    ICpuMetricsRepository cpuMetricsRepository,
                    ILogger<CpuMetricsController> logger,
                    IMapper mapper)
        {
            _cpuMetricsRepository = cpuMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _cpuMetricsRepository.Create(_mapper.Map<CpuMetric>(request));
            return Ok();
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<CpuMetric>> GetCpuMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get cpu metrics call.");

            return Ok(_cpuMetricsRepository.GetByTimePeriod(fromTime, toTime)
                .Select(metric => _mapper.Map<CpuMetricDto>(metric)).ToList());
        }
    }
}
