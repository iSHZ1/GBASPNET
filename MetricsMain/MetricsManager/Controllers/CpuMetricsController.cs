using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Services.Client;
using MetricsManager.Services.Repositorys;
using MetricsManager.Services.Repositorys.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MetricsManager.Controllers
{
    [Route("api/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private IHttpClientFactory _httpClientFactory;
        private ICpuMetricsManagerRepository _cpuMetricsRepository;
        private IAgentMetricsManagerRepository _agentMetricsManagerRepository;
        private IMetricsAgentClient _metricsAgentClient;

        public CpuMetricsController(
            IMetricsAgentClient metricsAgentClient,
            IHttpClientFactory httpClientFactory,
            ICpuMetricsManagerRepository cpuMetricsRepository,
            IAgentMetricsManagerRepository agentMetricsManagerRepository) 
        {
            _httpClientFactory = httpClientFactory;
            _metricsAgentClient = metricsAgentClient;
            _cpuMetricsRepository = cpuMetricsRepository;
            _agentMetricsManagerRepository = agentMetricsManagerRepository;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public ActionResult<CpuMetricsResponse> GetMetricsFromAgent(
            [FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {


            CpuMetricsResponse result = _metricsAgentClient.GetCpuMetrics(new CpuMetricsRequest
            {
                AgentId = agentId,
                FromTime = fromTime,
                ToTime = toTime
            });



            return Ok();
        }

        [HttpGet("all/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAll(
            [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
