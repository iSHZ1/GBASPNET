﻿using MetricsAgent.Converters;
using MetricsAgent.Models;
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
        #region Services

        private readonly ILogger<CpuMetricsController> _logger;
        private readonly ICpuMetricsRepository _cpuMetricsRepository;
        #endregion


        public CpuMetricsController(
            ICpuMetricsRepository cpuMetricsRepository,
            ILogger<CpuMetricsController> logger)
        {
            _cpuMetricsRepository = cpuMetricsRepository;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _cpuMetricsRepository.Create(new Models.CpuMetric
            {
                Value = request.Value,
                Time = (int)request.Time.TotalSeconds
            });
            return Ok();
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<CpuMetric>> GetCpuMetrics(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {

            _logger.LogInformation("Get cpu metrics call.");
            return Ok(_cpuMetricsRepository.GetByTimePeriod(fromTime, toTime));
        }
        [HttpDelete("Delete")]
        public IActionResult Delete([FromQuery] int id)
        {
            _cpuMetricsRepository.Delete(id);
            return Ok();
        }
        [HttpGet("GetAll")]
        public ActionResult<IList<CpuMetric>> GetAll()
        {
            return Ok(_cpuMetricsRepository.GetAll());
        }
        [HttpGet("GetById")]
        public ActionResult<CpuMetric> GetById([FromQuery] int id)
        {
            return Ok(_cpuMetricsRepository.GetById(id));
        }
        [HttpPut("Update")]
        public IActionResult Update([FromQuery]CpuMetric item)
        {
            _cpuMetricsRepository.Update(item);
            return Ok();
        }




    }
}