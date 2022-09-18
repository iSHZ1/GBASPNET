using MetricsManager.Models;
using MetricsManager.Models.Requests;
using MetricsManager.Services.Repositorys;
using MetricsManager.Services.Repositorys.Impl;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace MetricsManager.Services.Client.Impl
{
    public class MetricsAgentClient : IMetricsAgentClient
    {

        #region Services

        private readonly IAgentMetricsManagerRepository _agentMetricsRepository;
        private readonly HttpClient _httpClient;

        #endregion

        public MetricsAgentClient(
            HttpClient httpClient,
            IAgentMetricsManagerRepository agentMetricsRepository)
        {
            _httpClient = httpClient;
            _agentMetricsRepository = agentMetricsRepository;
        }


        public CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest request)
        {
            AgentInfo agentInfo = _agentMetricsRepository.GetById(request.AgentId);
            if (agentInfo == null)
                return null;

            string requestStr =
                $"{agentInfo.AgentAddress}api/metrics/cpu/from/{request.FromTime.ToString("dd\\.hh\\:mm\\:ss")}/to/{request.ToTime.ToString("dd\\.hh\\:mm\\:ss")}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestStr);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseStr = response.Content.ReadAsStringAsync().Result;
                CpuMetricsResponse cpuMetricsResponse =
                    (CpuMetricsResponse)JsonConvert.DeserializeObject(responseStr, typeof(CpuMetricsResponse));
                cpuMetricsResponse.AgentId = request.AgentId;
                return cpuMetricsResponse;
            }

            return null;
        }
    }
}
